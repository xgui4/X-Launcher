#!/usr/bin/env python3

import os
import platform
import sys

from PySide6.QtCore import QCoreApplication, Qt
from PySide6.QtGui import QIcon, QPixmap
from PySide6.QtWidgets import QApplication, QDialog, QMainWindow, QMessageBox, QSystemTrayIcon 
from frontend import version, name, author, url, license_text, description, channel, commit

import server.core_connector as core_connector
import ui.sys_tray  as sys_tray
from utils.json_trans import Translator
from ui.sys_tray import SysTray, SysTrayMenu
from ui.browser_dialog import BrowserDialog

from datetime import datetime

# System-specific patches for BSD in 2026
if platform.system() in ["FreeBSD", "GhostBSD"]:
    sys_tray.patch_freebsd_sys_tray()

if platform.system() == "Linux": 
    _LINUX_QT6_PATH = "/usr/lib/qt6/plugins"
    os.environ["QT_PLUGIN_PATH"] = _LINUX_QT6_PATH

if platform.system() == "FreeBSD": 
   _FREEBSD_QT6_PATH = "/usr/local/lib/qt6/plugins"
   os.environ["QT_PLUGIN_PATH"] = _FREEBSD_QT6_PATH

APP_NAME_STR = name
APP_VERSION_STR = version
AUTHOR_STR = author

translator: Translator = Translator(selectedLang="fr")

from launcher_ui import Ui_MainWindow
from about_ui import Ui_AboutDialog

import ressources_rc 

class MyWindow(QMainWindow):
    def news_site(self):
        BrowserDialog(self, "https://github.com/xgui4/X-Launcher/discussions")
    def __init__(self):
        super().__init__()

        self.ui = Ui_MainWindow()
        self.aboutDiag = QDialog()
        self.about = Ui_AboutDialog()

        self.ui.setupUi(self)
        self.about.setupUi(self.aboutDiag)

        app_icon = QPixmap(":/assets/app-icon.png");

        self.about.icon.setPixmap(app_icon)
        self.about.title.setText(name)
        self.about.versionLabel.setText(version)
        self.about.urlLabel.setText(url)
        self.about.creditsText.setText(author)
        self.about.copyLabel.setText("Copyleft (C) Xgui4")
        self.about.commitLabel.setText(f"Commit : {commit}")
        self.about.buildDateLabel.setText(f"Build date : {datetime.now()}")
        self.about.channelLabel.setText(f"Channel : {channel}")
        self.about.platformLabel.setText(f"Platform : {platform.release()}")
        self.about.licenseText.setText(license_text)

        window_title: str = translator.translate(key="App Title")
        msg_body: str = translator.translate(key="Message From Launcher")

        self.ui.actionLaunchInstance.triggered.connect(lambda:QMessageBox.information(
                self,
                window_title,
                f"{msg_body} : {core_connector.connect_to_server()}"
            )
        )
        
        app : QCoreApplication = QApplication.instance() # type: ignore

        self.ui.actionCloseWindow.triggered.connect(app.quit)
        self.ui.actionMoreNews.triggered.connect(self.news_site)
        self.ui.actionAbout.triggered.connect(self.aboutDiag.show)
        self.ui.actionAbout.setText(f"About {name}")

def main() -> None:
    
    app: QApplication = QApplication(sys.argv)

    window = MyWindow()
    window.show()

    if QSystemTrayIcon.isSystemTrayAvailable():
        app.setQuitOnLastWindowClosed(False)

    if platform.system() in ["FreeBSD", "GhostBSD"]:
        sys_tray.set_theme_after_patch(app)

    _ = QApplication.setStyle("breeze")

    tray: SysTray = sys_tray.SysTray(translator)
    menu: SysTrayMenu = sys_tray.SysTrayMenu(
        toggle_label="Toogle",
        about_label=tray.about_label,
        about_qt_label=tray.about_qt_label,
    )

    sys_tray.connect_menu_to_systray(menu, tray.quit_label, app, tray, window)
    tray.send_msg(
        title=tray.tray_title,
        msg=tray.tray_msg,
        icon=QIcon(":/assets/app-icon.ico"),
    )

    def show_about() -> None:
        QMessageBox.about(
            window,
            f"{tray.about_label} {tray.tray_title}",
            f"{APP_NAME_STR} v.{APP_VERSION_STR}\nCreated by {AUTHOR_STR}",
        )

    def show_about_qt() -> None:
        QMessageBox.aboutQt(window, title=f"{tray.about_label} {tray.tray_title}")

    menu.connect_app_window_to_systray(show_about, show_about_qt)

    sys.exit(app.exec())

if __name__ == "__main__":
    print("Launching X Launcher Core QT App")
    main()
