#!/usr/bin/env python3

import subprocess
import sys
import os
import tkinter as tk
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


def show_msgbox(title, msg):
    root = tk.Tk()
    root.withdraw()
    messagebox.showinfo(title, msg)
    root.destroy()


def run_dotnet(project_path):
    subprocess.run(
        ["dotnet", "run", "--framework", "net9.0", "--project", project_path],
        check=True
    )
    
def run_python(project_path):
    subprocess.run(
        ["python", project_path],
        check=True
    )

def main(): 
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
        run_python(QT_APP)

    else:
        print("Invalid option.")
        sys.exit(1)


if __name__ == "__main__":
    main()