#!/usr/bin/env python3

import os
import platform
import sys

from PySide6.QtGui import QIcon
from PySide6.QtWidgets import QApplication, QMainWindow, QMessageBox, QStyle, QSystemTrayIcon, QPushButton
from __init__ import version, name, author

from utils.json_trans import Translator
import server.core_connector as core_connector
import ui.sys_tray  as sys_tray
from ui.sys_tray import SysTray, SysTrayMenu
import utils.utils as utils

# System-specific patches for BSD in 2026
if platform.system() in ["FreeBSD", "GhostBSD"]:
    sys_tray.patch_freebsd_sys_tray()

APP_NAME_STR = name

APP_VERSION_STR = version

AUTHOR_STR = author

from launcher_ui import Ui_MainWindow
# Import the compiled resource file
import ressources_rc 

class MyWindow(QMainWindow):
    def __init__(self):
        super().__init__()
        # 1. Créer une instance de l'UI générée
        self.ui = Ui_MainWindow()
        # 2. L'initialiser en lui passant 'self' (la QMainWindow)
        self.ui.setupUi(self)

def main() -> None:
    
    app: QApplication = QApplication(sys.argv)

    window = MyWindow()
    window.show()

    if QSystemTrayIcon.isSystemTrayAvailable():
        app.setQuitOnLastWindowClosed(False)

    translator: Translator = Translator(selectedLang="fr")

    if platform.system() in ["FreeBSD", "GhostBSD"]:
        sys_tray.set_theme_after_patch(app)

    _ = QApplication.setStyle("breeze")

    tray: SysTray = sys_tray.SysTray(translator)
    menu: SysTrayMenu = sys_tray.SysTrayMenu(
        toggle_label="Toogle",
        about_label=tray.about_label,
        about_qt_label=tray.about_qt_label,
    )

    # window: MainWindow = MainWindow(tray, translator)
    # window.show()

    window_title: str = translator.translate(key="App Title")
    button_text: str = translator.translate(key="Main Button")
    msg_body: str = translator.translate(key="Message From Launcher")

    button: QPushButton = QPushButton(button_text)

    _NULL = button.clicked.connect(
        lambda: tray.showMessage(
            window_title,
            msg_body + " : " + core_connector.connect_to_server(),
            QIcon(os.path.join(utils.get_assets_dir(), "app-icon.ico")),
        )
    )

    sys_tray.connect_menu_to_systray(menu, tray.quit_label, app, tray, window)
    tray.send_msg(
        title=tray.tray_title,
        msg=tray.tray_msg,
        icon=QIcon(os.path.join(utils.get_assets_dir(), "app-icon.ico")),
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
