#!/usr/bin/env python3
import os
import sys
import platform
import sys_tray
import json_trans
import utils
from PySide6.QtWidgets import QApplication, QSystemTrayIcon, QMessageBox
from PySide6.QtGui import QIcon
from main_window import MainWindow

# System-specific patches for BSD in 2026
if platform.system() in ['FreeBSD', 'GhostBSD']:
    sys_tray.patch_freebsd_sys_tray()

def main() -> None:
    app = QApplication(sys.argv)
    
    if QSystemTrayIcon.isSystemTrayAvailable(): 
        app.setQuitOnLastWindowClosed(False)

    translator = json_trans.Translator("fr")
    
    if platform.system() in ['FreeBSD', 'GhostBSD']:
        sys_tray.set_theme_after_patch(app)

    QApplication.setStyle("breeze")

    tray = sys_tray.SysTray(translator)
    menu = sys_tray.SysTrayMenu("Toogle", tray.about_label, tray.about_qt_label)

    window = MainWindow(tray, translator)
    window.show()
    
    sys_tray.connect_menu_to_systray(menu, tray.quit_label, app, tray, window)
    tray.send_msg(tray.tray_title, tray.tray_msg, QIcon(os.path.join(utils.get_assets_dir() , "app-icon.ico")))
            
    def show_about():
        QMessageBox.about(window, f"{tray.about_label} {tray.tray_title}", "X-Launcher v.0.0.1\nCreated by Xgui4")

    def show_about_qt():
        QMessageBox.aboutQt(window, f"{tray.about_label} {tray.tray_title}")
        
    menu.connect_app_window_to_systray(show_about, show_about_qt)

    sys.exit(app.exec())

if __name__ == "__main__":
    main()
