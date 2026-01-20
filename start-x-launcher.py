#!/usr/bin/env python3
import subprocess
import os
from subprocess import Popen

PROJECT_DIR = os.path.abspath(os.path.dirname(__file__))

QT_APP = os.path.join(
    PROJECT_DIR,
    "src",
    "app.py"
)

BACKEND = os.path.join(
    PROJECT_DIR,
    "backend",
    "X_Launcher.Service",
    "X_Launcher.Service.csproj"
)
def run_python_parallel(project_path : str) -> Popen[bytes]:
    return subprocess.Popen(
        ["python", project_path],
    )

def run_dotnet_parallel(project_path : str) -> Popen[bytes]:
    return subprocess.Popen(
        ["dotnet", "run", "--framework", "net9.0", "--project", project_path],
    )

def main() -> None: 
    print("Launching X Launcher Startup Script Pre-Alpha")
    p1 = run_python_parallel(QT_APP)
    p2 = run_dotnet_parallel(BACKEND)
    p1.wait() 
    p2.wait()

if __name__ == "__main__":
    main()