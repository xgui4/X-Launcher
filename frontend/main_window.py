from PySide6.QtCore import Qt
from PySide6.QtWidgets import QApplication, QMainWindow, QPushButton, QSystemTrayIcon, QMenu, QMessageBox, QSystemTrayIcon
from PySide6.QtGui import QIcon
import json_trans

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
