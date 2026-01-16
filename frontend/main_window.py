import os
import utils
from typing import Optional 
from PySide6.QtWidgets import  QWidget, QMainWindow, QPushButton, QSystemTrayIcon, QVBoxLayout, QLabel
from PySide6.QtGui import QIcon, QPixmap
from PySide6.QtCore import Qt
from json_trans import Translator

class MainWindow(QMainWindow):
    def __init__(self, tray : QSystemTrayIcon, translator : Translator, parent : Optional[QWidget] = None):
        super().__init__(parent)
        
        window_title : str = translator.translate("App Title")
        button_text: str = translator.translate("Main Button")
        msg_body = translator.translate("Hello World")

        self.setWindowTitle(window_title)
        button = QPushButton(button_text)
        
        button.clicked.connect(lambda: tray.showMessage(
            window_title, 
            msg_body, 
            QIcon(os.path.join(utils.get_assets_dir(), "app-icon.ico"))
        ))
        
        layout = QVBoxLayout(self)
        
        central_widget = QWidget()

        self.setCentralWidget(central_widget)
        
        layout.addWidget(button)
        
        image_widget = QLabel()
        
        image_widget.setPixmap(QPixmap(os.path.join(utils.get_assets_dir(), "placeholder.jpg")))
        
        layout.addWidget(image_widget)
        
        central_widget.setLayout(layout)
        
        self.setWindowFlags(Qt.Window | Qt.Tool)  # type: ignore