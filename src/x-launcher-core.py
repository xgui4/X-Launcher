#!/usr/bin/env python3

import os
from subprocess import Popen

from frontend.utils import loggers
from frontend.utils import utils

PROJECT_DIR: str = utils.get_project_root()

FRONTEND_FOLDER: str = "frontend"
QT_APP_EXE: str = "app.py"

TARGET_FOLDER: str = "target"
BACKEND_FOLDER: str = "backend"
BACKEND_PROJECT_FOLDER: str = "X_Launcher.Service"
BACKEND_PROJECT_FILE: str = "X_Launcher.Service.csproj"

QT_APP: str = os.path.join(
    PROJECT_DIR, TARGET_FOLDER, FRONTEND_FOLDER, QT_APP_EXE
)

BACKEND: str = os.path.join(
    PROJECT_DIR, TARGET_FOLDER, BACKEND_FOLDER, BACKEND_PROJECT_FOLDER, BACKEND_PROJECT_FILE
)

NO_ERROR: str = "None"

def run_python_parallel(project_path: str) -> Popen[bytes]:
    python_exe: str = "python"
    return Popen[bytes](
        [python_exe, project_path],
    )

def run_dotnet_parallel(dll_exe_path: str) -> Popen[bytes]:
    dotnet_exe: str = "dotnet"
    return Popen[bytes](
        [ dotnet_exe, dll_exe_path ],
    )

def main() -> None:
    logger: loggers.BasicLogger = loggers.BasicLogger()

    frontend_name: str = "X Launcher Core QT App"
    backend_name: str = "X Launcher Service"

    exit_no_error_str: str = "Exited without any issue"
    exit_with_code_str: str = "Exited with Code"

    print("Launching X Launcher Startup Script 0.0.3dev")

    try: 
        frontend: Popen[bytes] = run_python_parallel(project_path=QT_APP)
        backend: Popen[bytes] = run_dotnet_parallel(dll_exe_path=BACKEND)

        stdout1, stderr1 = frontend.communicate()

        if str(stdout1) != NO_ERROR:
            logger.info(msg=f"{frontend_name} : {stdout1}")
        if str(stderr1) != NO_ERROR:
            logger.error(msg=f"{frontend_name}: {stderr1}")
        else:
            logger.info(msg=f"{frontend_name} {exit_no_error_str}")

        stdout2, stderr2 = backend.communicate()

        if str(stdout2) != NO_ERROR:
            logger.info(msg=f"{backend_name} : {stdout2}")
        if str(stderr2) != NO_ERROR:
            logger.error(msg=f"{backend_name} : {stderr2}")
        else:
            logger.info(msg=f"{backend_name} {exit_no_error_str}")

        frontend_exit_code: int = frontend.returncode
        logger.info(msg=f"{frontend_name} {exit_with_code_str}: {frontend_exit_code}")

        backend_exit_code: int = backend.returncode
        logger.info(msg=f"{backend_name} {exit_with_code_str}:  {backend_exit_code}")
    except Exception as e:
        logger.error(msg=f"An error occurred in x-launcher-core.py : {e}")

if __name__ == "__main__":
    main()
