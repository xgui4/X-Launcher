import os
from typing import Callable, Optional 
from PySide6.QtWidgets import QWidget, QApplication, QSystemTrayIcon, QMenu
from PySide6.QtGui import QIcon
import utils
from json_trans import Translator

def patch_freebsd_sys_tray() -> None :
    ''' Specific Patch for FreeBSD'''
    # Force X11 backend (disables Wayland attempts)
    os.environ["QT_QPA_PLATFORM"] = "xcb"

    # Trick KDE plugin into falling back to standard X11 tray protocols
    os.environ["KDE_FULL_SESSION"] = ""
    os.environ["XDG_CURRENT_DESKTOP"] = "XFCE"  # Using 'XFCE' or 'GNOME' forces XEmbed fallback

    # Disable the portal error causing the crash
    os.environ["QT_NO_XDG_DESKTOP_PORTAL"] = "1"


def set_theme_after_patch(app : QApplication) -> None:
    app.setStyle("breeze")
    
    
class SysTrayMenu(QMenu): 
    def __init__(self,toggle_label : str, about_label : str, about_qt_label : str , parent : Optional[QWidget] = None) -> None :
        super().__init__(parent)
        self.toggle_action = self.addAction(toggle_label)
        self.about_action = self.addAction(about_label)
        self.about_qt_action = self.addAction(about_qt_label)
        self.quit_action = None
    
    def connect_app_window_to_systray(self, show_about: Callable[[], None], show_about_qt: Callable[[], None]) -> None:
        self.about_action.triggered.connect(show_about)
        self.about_qt_action.triggered.connect(show_about_qt)
    
    
class SysTray(QSystemTrayIcon) :
    def __init__(self, translator : Translator, parent: Optional[QWidget] = None):
        super().__init__(parent)
        self.about_label = translator.translate("About SysTray")
        self.about_qt_label = f"{self.about_label} Qt"
        self.quit_label = translator.translate("Quit SysTray")
        self.tray_title = translator.translate("App Title") 
        self.tray_msg = translator.translate("Hello World")   
        self.setIcon(QIcon(os.path.join(utils.get_assets_dir(), "app-icon.ico")))
        self.setVisible(True)


    def create_sys_tray(self, menu : SysTrayMenu, app : QApplication, window : QWidget):
        menu.connect_menu_to_systray(self.quit_label, app, self, window) 
     

    def send_msg(self, title : str , msg : str, icon : QIcon):
        self.showMessage(title, msg, icon)
        
        
def connect_menu_to_systray(menu : SysTrayMenu, quit_label : str, app : QApplication, tray : SysTray, window : QWidget) -> None :
    menu.quit_action = menu.addAction(quit_label)
    menu.quit_action.triggered.connect(app.quit)
    menu.toggle_action.triggered.connect(
        lambda: 
            window.hide() if window.isVisible() 
            else window.show()
    )
    tray.setContextMenu(menu)