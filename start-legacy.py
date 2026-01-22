#!/usr/bin/env python3

import os
import subprocess
import sys
from tkinter import messagebox

from src.frontend import utils

STARTUP_STR: str = "Launching X Launcher Startup Script Pre-Alpha"

MENU_STR: str ="""
        Choose an option:
        1. Launch X Launcher CLI (LEGACY)
        2. Launch X Launcher Desktop (LEGACY)
        3. Quit
        > """

PROJECT_DIR: str = utils.get_project_root()

CLI_PROJECT: str = os.path.join(
    PROJECT_DIR, "src", "legacy", "X_Launcher.CLI", "X_Launcher.CLI.csproj"
)

DESKTOP_PROJECT: str = os.path.join(
    PROJECT_DIR, "src", "legacy", "X_Launcher.Desktop", "X_Launcher.Desktop.csproj"
)

BACKEND: str = os.path.join(
    PROJECT_DIR, "src", "backend", "X_Launcher.Service", "X_Launcher.Service.csproj"
)


def show_msgbox(title: str, msg: str) -> None:
    _NULL: str = messagebox.showinfo(title, message=msg)


def run_dotnet(project_path: str) -> None:
    print(
        subprocess.run(
            ["dotnet", "run", "--framework", "net9.0", "--project", project_path],
            check=True,
        )
    )


def run_python(project_path: str) -> None:
    print(subprocess.run(["python", project_path], check=True))


def run_python_parallel(project_path: str) -> None:
    print(
        subprocess.Popen(
            ["python", project_path],
        )
    )


def run_dotnet_parallel(project_path: str) -> None:
    print(
        subprocess.Popen(
            ["dotnet", "run", "--framework", "net9.0", "--project", project_path],
        )
    )


def main() -> None:
    print(STARTUP_STR)

    user_input: str = input(
        MENU_STR
    ).strip()

    if user_input == "1":
        run_dotnet(project_path=CLI_PROJECT)

    elif user_input == "2":
        run_dotnet(project_path=DESKTOP_PROJECT)

    elif user_input == "3":
        exit()

    else:
        print("Invalid option.")
        sys.exit(1)


if __name__ == "__main__":
    main()
