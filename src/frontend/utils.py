import os
from pathlib import Path


def get_frontend_dir() -> str:
    return os.path.abspath(os.path.dirname(__file__))


def get_project_root() -> str:
    dir: Path = Path(get_frontend_dir())
    return str((dir.parent.parent))


def get_locales_dir() -> str:
    return os.path.join(get_project_root(), "locales")


def get_assets_dir() -> str:
    return os.path.join(get_project_root(), "assets")
