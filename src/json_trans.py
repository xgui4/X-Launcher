import json
import os
import utils
from PySide6 import QtCore # type: ignore  # pyright: ignore[reportMissingTypeStubs]

class Translator(QtCore.QObject):

    languageChanged: QtCore.Signal = QtCore.Signal()  # Signal QT about the language changes
    
    FALLBACK_LANG: str = "en"

    def __init__(self, selectedLang : str = FALLBACK_LANG) -> None:
        super().__init__()
        self._data : dict[str, str] = {}
        self._current_lang : str = selectedLang
        self.load_lang(selectedLang)

    def load_lang(self, lang_code : str):
        """Loads a local JSON file (e.g., locales/en.json)"""
        
        file_path = f"{utils.get_locales_dir()}/{lang_code}.json"

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

    def translate(self, key : str, default : str ="") -> str:
        """Returns the translated string or the key itself if not found"""
        return self._data.get(key, default or key)
