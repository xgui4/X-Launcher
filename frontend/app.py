#!/usr/bin/env python3

import sys
import os
import platform
import sys_tray
import json_trans
from PySide6.QtCore import Qt
from PySide6.QtWidgets import QApplication, QMainWindow, QPushButton, QSystemTrayIcon, QMenu, QMessageBox
from PySide6.QtGui import QIcon
from main_window import MainWindow

if platform.system() == 'FreeBSD' or platform.system() == "GhostBSD":
    sys_tray.patch_freebsd_sys_tray()


def main():
    
    app = QApplication(sys.argv)
    
    if (QSystemTrayIcon.isSystemTrayAvailable()): 
        app.setQuitOnLastWindowClosed(False)

    translator = json_trans.Translator("fr")
    
    if platform.system() == 'FreeBSD':
        sys_tray.set_theme_after_patch(app) 
            
    about_label = translator.translate("About SysTray")
    about_qt_label = translator.translate("About SysTray") + " Qt"
    quit_label = translator.translate("Quit SysTray")
    tray_title = translator.translate("App Title")
    tray_msg = translator.translate("Hello World")

    tray = QSystemTrayIcon()
    tray.setIcon(QIcon("/home/xgui4/develop/X-Launcher/assets/app-icon.ico"))
    tray.setVisible(True)

    menu = QMenu()
    about_action = menu.addAction(about_label)
    about_qt_action = menu.addAction(about_qt_label)

    quit_action = menu.addAction(quit_label)
    quit_action.triggered.connect(app.quit)
    tray.setContextMenu(menu)

    tray.showMessage(tray_title, tray_msg, QIcon("/home/xgui4/develop/X-Launcher/assets/app-icon.ico"))
        
    window = MainWindow(tray, translator)
    window.show()

    def show_about():
        QMessageBox.about(window, f"{about_label} {tray_title}", "X-Launcher v.0.0.1\nCreated by Xgui4")

    def show_about_qt():
        QMessageBox.aboutQt(window, f"{about_label} {tray_title}")
        
    about_action.triggered.connect(show_about)
    about_qt_action.triggered.connect(show_about_qt)

    sys.exit(app.exec())

if __name__ == "__main__":
    main()
