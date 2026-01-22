import os

from PySide6.QtGui import QIcon, QPixmap
from PySide6.QtWidgets import (
    QLabel,
    QMainWindow,
    QPushButton,
    QSystemTrayIcon,
    QVBoxLayout,
    QWidget,
)

import server.core_connector as core_connector
from utils.json_trans import Translator 
import utils.utils as utils 


class MainWindow(QMainWindow):
    def __init__(
        self, tray: QSystemTrayIcon, translator: Translator, parent: None = None
    ) -> None:
        super().__init__(parent)

        window_title: str = translator.translate(key="App Title")
        button_text: str = translator.translate(key="Main Button")
        msg_body: str = translator.translate(key="Message From Launcher")

        self.setWindowTitle(window_title)
        button: QPushButton = QPushButton(button_text)

        _NULL = button.clicked.connect(
            lambda: tray.showMessage(
                window_title,
                msg_body + " : " + core_connector.connect_to_server(),
                QIcon(os.path.join(utils.get_assets_dir(), "app-icon.ico")),
            )
        )

        layout: QVBoxLayout = QVBoxLayout(self)

        central_widget: QWidget = QWidget()

        self.setCentralWidget(central_widget)

        banner_widget: QLabel = QLabel()

        banner_widget.setPixmap(
            QPixmap(os.path.join(utils.get_assets_dir(), "banner.png"))
        )

        layout.addWidget(banner_widget)

        layout.addWidget(button)

        image_widget: QLabel = QLabel()

        image_widget.setPixmap(
            QPixmap(os.path.join(utils.get_assets_dir(), "placeholder.jpg"))
        )

        layout.addWidget(image_widget)

        central_widget.setLayout(layout)

        # self.setWindowFlags(Qt.Window | Qt.Tool)
