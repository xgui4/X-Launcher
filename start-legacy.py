#!/usr/bin/env python3

import os
import subprocess
import sys
from tkinter import messagebox

PROJECT_DIR = os.path.abspath(os.path.dirname(__file__))

CLI_PROJECT = os.path.join(
    PROJECT_DIR,
    "legacy",
    "X_Launcher.CLI",
    "X_Launcher.CLI.csproj"
)

DESKTOP_PROJECT = os.path.join(
    PROJECT_DIR,
    "legacy",
    "X_Launcher.Desktop",
    "X_Launcher.Desktop.csproj"
)

QT_APP = os.path.join(
    PROJECT_DIR,
    "frontend",
    "app.py"
)

BACKEND = os.path.join(
    PROJECT_DIR,
    "backend",
    "X_Launcher.Service",
    "X_Launcher.Service.csproj"
)

def show_msgbox(title : str, msg : str) -> None:
    _NULL= messagebox.showinfo(title, msg)

def run_dotnet(project_path : str) -> None:
    print(subprocess.run(
        ["dotnet", "run", "--framework", "net9.0", "--project", project_path],
        check=True
    ))

def run_python(project_path : str) -> None:
    print(subprocess.run(
        ["python", project_path],
        check=True
    ))

def run_python_parallel(project_path : str) -> None:
    print(subprocess.Popen(
        ["python", project_path],
    ))

def run_dotnet_parallel(project_path : str) -> None:
    print(subprocess.Popen(
        ["dotnet", "run", "--framework", "net9.0", "--project", project_path],
    ))

def main() -> None:
    print("Launching X Launcher Startup Script Pre-Alpha")

    user_input = input(
        """
        Choose an option:
        1. Launch X Launcher CLI (LEGACY)
        2. Launch X Launcher Desktop (LEGACY)
        3. [Coming Soon] Launch X Launcher QT (Experimental Core)
        > """
    ).strip()

    if user_input == "1":
        run_dotnet(CLI_PROJECT)

    elif user_input == "2":
        run_dotnet(DESKTOP_PROJECT)

    elif user_input == "3":
        show_msgbox("X Launcher Startup Script", "Work in progress")
        run_python_parallel(QT_APP)
        run_dotnet_parallel(BACKEND)

    else:
        print("Invalid option.")
        sys.exit(1)

if __name__ == "__main__":
    main()