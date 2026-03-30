from PySide6.QtWidgets  import QDialog, QHBoxLayout, QWidget
from PySide6.QtWebEngineWidgets import QWebEngineView
from PySide6.QtCore import QUrl

from frontend import name

class BrowserDialog(QDialog):
    def __init__(self, parent : QWidget, url : str):
        super().__init__()
        browser_dialog = QDialog(parent)
        webview = QWebEngineView()
        webview.load(QUrl(url))
        website_title: str = webview.title()
        browser_dialog.setWindowTitle(f"{website_title} - {name}")
        layout = QHBoxLayout()
        layout.addWidget(webview)
        browser_dialog.setLayout(layout)
        browser_dialog.show()