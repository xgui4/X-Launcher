from configparser import ConfigParser
import configparser
import os

import utils.utils as utils


class Config:
    CONFIG_FILENAME: str = "config.ini"
    INTERFACE_CONFIG: str = "interface"
    THEME_CONFIG: str = "Theme"
    LANG_CONFIG: str = "Lang"

    def __init__(self) -> None:
        self.config: ConfigParser = configparser.ConfigParser()

    def gen_config(self) -> None:
        self.config[self.INTERFACE_CONFIG] = {
            self.THEME_CONFIG: "plasma",
            self.LANG_CONFIG: "en",
        }

        with open(file=self.CONFIG_FILENAME, mode="w", encoding="utf-8") as configfile:
            self.config.write(fp=configfile)

    def read_config(self, key: str, path: str | None = None) -> str:
        if path == None:
            path = utils.get_frontend_dir() or ""

        full_path: str = os.path.join(path, self.CONFIG_FILENAME)

        files_loaded: list[str] = self.config.read(filenames=full_path)

        if not files_loaded:
            raise FileNotFoundError(f"Could not find or read: {full_path}")

        return self.config.get(self.INTERFACE_CONFIG, key)
