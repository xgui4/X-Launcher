#!/usr/bin/env python3

import os
import platform
import sys

from PySide6.QtGui import QIcon
from PySide6.QtWidgets import QApplication, QMessageBox, QStyle, QSystemTrayIcon

from utils.json_trans import Translator
from ui.main_window import MainWindow
import ui.sys_tray  as sys_tray
from ui.sys_tray import SysTray, SysTrayMenu
import utils.utils as utils

# System-specific patches for BSD in 2026
if platform.system() in ["FreeBSD", "GhostBSD"]:
    sys_tray.patch_freebsd_sys_tray()


def main() -> None:
    app: QApplication = QApplication(sys.argv)

    if QSystemTrayIcon.isSystemTrayAvailable():
        app.setQuitOnLastWindowClosed(False)

    translator: Translator = Translator(selectedLang="fr")

    if platform.system() in ["FreeBSD", "GhostBSD"]:
        sys_tray.set_theme_after_patch(app)

    _NULL: QStyle | None = QApplication.setStyle("breeze")

    tray: SysTray = sys_tray.SysTray(translator)
    menu: SysTrayMenu = sys_tray.SysTrayMenu(
        toggle_label="Toogle",
        about_label=tray.about_label,
        about_qt_label=tray.about_qt_label,
    )

    window: MainWindow = MainWindow(tray, translator)
    window.show()

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
            "X-Launcher v.0.0.1\nCreated by Xgui4",
        )

    def show_about_qt() -> None:
        QMessageBox.aboutQt(window, title=f"{tray.about_label} {tray.tray_title}")

    menu.connect_app_window_to_systray(show_about, show_about_qt)

    sys.exit(app.exec())


if __name__ == "__main__":
    main()
