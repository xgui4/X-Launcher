import sys
import os
import json_trans
from PySide6.QtCore import Qt
from PySide6.QtWidgets import QApplication, QMainWindow, QPushButton, QSystemTrayIcon, QMenu, QMessageBox, QSystemTrayIcon
from PySide6.QtGui import QIcon

def patch_freebsd_sys_tray():
    ''' Specific Patch for FreeBSD'''
    # Force X11 backend (disables Wayland attempts)
    os.environ["QT_QPA_PLATFORM"] = "xcb"

    # Trick KDE plugin into falling back to standard X11 tray protocols
    os.environ["KDE_FULL_SESSION"] = ""
    os.environ["XDG_CURRENT_DESKTOP"] = "XFCE"  # Using 'XFCE' or 'GNOME' forces XEmbed fallback

    # Disable the portal error causing the crash
    os.environ["QT_NO_XDG_DESKTOP_PORTAL"] = "1"


def set_theme_after_patch(app : QApplication):
    app.setStyle("breeze")
    

def create_sys_tray(translator, app):
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