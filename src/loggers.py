import logging
from logging import FileHandler, Formatter, Logger
import os

from platformdirs import user_log_dir

USER_LOG_PATH: str = user_log_dir(
    appname="X_Laucher_Core", appauthor="Xgui4_Studio", version="pre-alpha"
)

LOG_FILE: str = os.path.join(USER_LOG_PATH, "launcher.log")
DEFAULT_LEVEL: int = logging.INFO
DEFAULT_NAME: str = "x_launcher_core"
DEFAULT_LOG_FORMAT: str = "%(asctime)s - %(levelname)s - %(message)s"


class BasicLogger:
    def __init__(
        self,
        log_file: str = LOG_FILE,
        logger_name: str = DEFAULT_NAME,
        level: int = DEFAULT_LEVEL,
        format_str: str = DEFAULT_LOG_FORMAT,
    ) -> None:

        os.makedirs(name=os.path.dirname(log_file), exist_ok=True)

        self.logger: Logger = logging.getLogger(name=logger_name)
        self.logger.setLevel(level)

        if not self.logger.handlers:
            file_handler: FileHandler = FileHandler(filename=log_file, encoding="utf-8")
            formatter: Formatter = Formatter(fmt=format_str)
            file_handler.setFormatter(fmt=formatter)

            self.logger.addHandler(hdlr=file_handler)

    def info(self, msg: str) -> None:
        self.logger.info(msg)

    def warning(self, msg: str) -> None:
        self.logger.warning(msg)

    def debug(self, msg: str) -> None:
        self.logger.debug(msg)

    def error(self, msg: str) -> None:
        self.logger.error(msg)
