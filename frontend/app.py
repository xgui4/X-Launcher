#!/usr/local/bin/python
import sys
import os
from PySide6.QtCore import Qt
from PySide6.QtWidgets import QApplication, QMainWindow, QPushButton, QSystemTrayIcon, QMenu
from PySide6.QtGui import QIcon
import json_trans  # Your custom file name works perfectly here

class MainWindow(QMainWindow):
    def __init__(self, tray, translator):
        super().__init__()
        
        window_title = translator.translate("App Title")
        button_text = translator.translate("Main Button")
        msg_body = translator.translate("Hello World")

        self.setWindowTitle(window_title)
        button = QPushButton(button_text)
        
        button.clicked.connect(lambda: tray.showMessage(
            window_title, 
            msg_body, 
            QIcon("/home/xgui4/develop/X-Launcher/assets/app-icon.ico")
        ))

        self.setCentralWidget(button)


# Force X11 backend (disables Wayland attempts)
os.environ["QT_QPA_PLATFORM"] = "xcb"

# Trick KDE plugin into falling back to standard X11 tray protocols
os.environ["KDE_FULL_SESSION"] = ""
os.environ["XDG_CURRENT_DESKTOP"] = "XFCE"  # Using 'XFCE' or 'GNOME' forces XEmbed fallback

# Disable the portal error causing the crash
os.environ["QT_NO_XDG_DESKTOP_PORTAL"] = "1"

def main():
    
    app = QApplication(sys.argv)
    
    if (QSystemTrayIcon.isSystemTrayAvailable()): 
        app.setQuitOnLastWindowClosed(False)

    translator = json_trans.Translator()
    
    app.setStyle("breeze") # 'breeze' here refers to the Qt plugin, not a hardcoded look
    
    about_label = translator.translate("About")
    quit_label = translator.translate("Quit")
    tray_title = translator.translate("App Title")  #
    tray_msg = translator.translate("Hello World")  

    tray = QSystemTrayIcon()
    tray.setIcon(QIcon("/home/xgui4/develop/X-Launcher/assets/app-icon.ico"))
    tray.setVisible(True)

    menu = QMenu()
    menu.addAction(about_label)
    quit_action = menu.addAction(quit_label)
    quit_action.triggered.connect(app.quit)
    tray.setContextMenu(menu)

    tray.showMessage(tray_title, tray_msg, QIcon("/home/xgui4/develop/X-Launcher/assets/app-icon.ico"))

    window = MainWindow(tray, translator)
    window.show()

    sys.exit(app.exec())

if __name__ == "__main__":
    main()
