import json
import os
from PySide6 import QtCore


class Translator(QtCore.QObject):

    languageChanged = QtCore.Signal()  # Signal QT about the language changes
    
    FALLBACK_LANG = "en"

    def __init__(self, selectedLang = FALLBACK_LANG):
        super().__init__()
        self._data = {}
        self._current_lang = selectedLang
        self.load_lang(selectedLang)

    def load_lang(self, lang_code):
        """Loads a local JSON file (e.g., languages/en.json)"""
        file_path = f"/home/xgui4/develop/X-Launcher/locales/{lang_code}.json"

        if not os.path.exists(file_path):
            print(f"Error: Language file {file_path} not found.")
            return

        try:
            with open(file_path, "r", encoding="utf-8") as f:
                self._data = json.load(f)
                self._current_lang = lang_code
                self.languageChanged.emit()  # Notify the UI
        except Exception as e:
            print(f"Error loading JSON: {e}")

    def translate(self, key, default=""):
        """Returns the translated string or the key itself if not found"""
        return self._data.get(key, default or key)
