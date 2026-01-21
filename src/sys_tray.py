import os
from typing import Callable
from PySide6.QtWidgets import QStyle, QWidget, QApplication, QSystemTrayIcon, QMenu
from PySide6.QtGui import QAction, QIcon
import utils
from json_trans import Translator


def patch_freebsd_sys_tray() -> None:
    """Specific Patch for FreeBSD"""
    # Trick KDE plugin into falling back to standard X11 tray protocols
    os.environ["KDE_FULL_SESSION"] = ""

    # Using 'XFCE' or 'GNOME' forces XEmbed fallback
    os.environ["XDG_CURRENT_DESKTOP"] = "XFCE"

    # Disable the portal error causing the crash
    os.environ["QT_NO_XDG_DESKTOP_PORTAL"] = "1"


def set_theme_after_patch(app: QApplication) -> None:
    _NULL: QStyle | None = app.setStyle("breeze")


class SysTrayMenu(QMenu):
    def __init__(
        self,
        toggle_label: str,
        about_label: str,
        about_qt_label: str,
        parent: None = None,
    ) -> None:
        super().__init__(parent)
        self.toggle_action: QAction = self.addAction(toggle_label)
        self.about_action: QAction = self.addAction(about_label)
        self.about_qt_action: QAction = self.addAction(about_qt_label)
        self.quit_action: None = None

    def connect_app_window_to_systray(
        self, show_about: Callable[[], None], show_about_qt: Callable[[], None]
    ) -> None:
        self.about_action.triggered.connect(
            show_about
        )  # pyright: ignore[reportUnusedCallResult]
        self.about_qt_action.triggered.connect(
            show_about_qt
        )  # pyright: ignore[reportUnusedCallResult]


class SysTray(QSystemTrayIcon):
    def __init__(self, translator: Translator, parent: None = None) -> None:
        super().__init__(parent)
        self.about_label: str = translator.translate(key="About SysTray")
        self.about_qt_label: str = f"{self.about_label} Qt"
        self.quit_label: str = translator.translate(key="Quit SysTray")
        self.tray_title: str = translator.translate(key="App Title")
        self.tray_msg: str = translator.translate(key="Hello World")
        self.setIcon(QIcon(os.path.join(utils.get_assets_dir(), "app-icon.ico")))
        self.setVisible(True)

    def create_sys_tray(
        self, menu: SysTrayMenu, app: QApplication, window: QWidget
    ) -> None:
        menu.connect_menu_to_systray(  # pyright: ignore[reportUnknownMemberType, reportAttributeAccessIssue]
            self.quit_label, app, self, window
        )  

    def send_msg(self, title: str, msg: str, icon: QIcon) -> None:
        self.showMessage(title, msg, icon)


def connect_menu_to_systray(
    menu: SysTrayMenu,
    quit_label: str,
    app: QApplication,
    tray: SysTray,
    window: QWidget,
) -> None:
    menu.quit_action = menu.addAction(  # pyright: ignore[reportAttributeAccessIssue]
        quit_label
    )  
    menu.quit_action.triggered.connect(  # pyright: ignore[reportUnknownMemberType, reportAttributeAccessIssue]
        app.quit
    )  
    _NULL = menu.toggle_action.triggered.connect(
        lambda: window.hide() if window.isVisible() else window.show()
    )
    tray.setContextMenu(menu)
