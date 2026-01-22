#!/usr/bin/env python3

import os
from subprocess import Popen

import src.loggers
import src.utils

PROJECT_DIR: str = src.utils.get_project_root()

FRONTEND_FOLDER: str = "src"
QT_APP_EXE: str = "app.py"

BACKEND_FOLDER: str = "backend"
BACKEND_PROJECT_FOLDER: str = "X_Launcher.Service"
BACKEND_PROJECT_FILE: str = "X_Launcher.Service.csproj"

DOTNET_FRAMEWORK: str = "net-9.0"

QT_APP: str = os.path.join(PROJECT_DIR, FRONTEND_FOLDER, QT_APP_EXE)

BACKEND: str = os.path.join(
    PROJECT_DIR, BACKEND_FOLDER, BACKEND_PROJECT_FOLDER, BACKEND_PROJECT_FILE
)

NO_ERROR: str = "None"


def run_python_parallel(project_path: str) -> Popen[bytes]:
    python_exe: str = "python"
    return Popen[bytes](
        [python_exe, project_path],
    )


def run_dotnet_parallel(project_path: str) -> Popen[bytes]:
    dotnet_exe: str = "dotnet"
    dotnet_option_str: str = "run"
    framework_option_str: str = "--framwework"
    project_option_str: str = "--project"
    return Popen[bytes](
        [
            dotnet_exe,
            dotnet_option_str,
            framework_option_str,
            DOTNET_FRAMEWORK,
            project_option_str,
            project_path,
        ],
    )


def main() -> None:
    logger: src.loggers.BasicLogger = src.loggers.BasicLogger()

    frontend_name: str = "X Launcher Core QT App"
    backend_name: str = "X Launcher Service"

    exit_no_error_str: str = "Exited without any issue"
    exit_with_code_str: str = "Exited with Code"

    print("Launching X Launcher Startup Script Pre-Alpha")

    p1: Popen[bytes] = run_python_parallel(project_path=QT_APP)
    p2: Popen[bytes] = run_dotnet_parallel(project_path=BACKEND)

    stdout1, stderr1 = p1.communicate()

    if str(stdout1) != NO_ERROR:
        logger.info(msg=f"{frontend_name} : {stdout1}")
    if str(stderr1) != NO_ERROR:
        logger.error(msg=f"{frontend_name}: {stderr1}")
    else:
        logger.info(msg=f"{frontend_name} {exit_no_error_str}")

    stdout2, stderr2 = p2.communicate()

    if str(stdout2) != NO_ERROR:
       logger.info(msg=f"{backend_name} : {stdout2}")
    if str(stderr2) != NO_ERROR:
        logger.error(msg=f"{backend_name} : {stderr2}")
    else:
        logger.info(msg=f"{backend_name} {exit_no_error_str}")

    frontend_exit_code: int = p1.returncode
    logger.info(msg=f"{frontend_name} {exit_with_code_str}: {frontend_exit_code}")

    backend_exit_code: int = p2.returncode
    logger.info(msg=f"{backend_name} {exit_with_code_str}:  {backend_exit_code}")


if __name__ == "__main__":
    main()
