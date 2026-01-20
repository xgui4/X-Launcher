#!/usr/bin/env python3
import subprocess
import os
import logging
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
    # Configure logging to write to 'app.log'
    # The default filemode is 'a' (append), so new logs are added to the end
    logging.basicConfig(
        filename='laucher.log',
        level=logging.INFO, # Log messages with severity INFO or higher
        format='%(asctime)s - %(levelname)s - %(message)s' # Format the log messages
    )

    print("Launching X Launcher Startup Script Pre-Alpha")
    p1 = run_python_parallel(QT_APP)
    p2 = run_dotnet_parallel(BACKEND)
    
    # This reads output AND waits for the process to exit
    stdout1, stderr1 = p1.communicate()
    
    logging.info(msg=f"frontend : {stdout1}")
    logging.error(msg=f"frontend : {stderr1}")
    
    stdout2, stderr2 = p2.communicate()
    
    logging.info(msg=f"backend : {stdout2}")
    logging.error(msg=f"backend : {stderr2}")
    
    frontend_exit_code:int = p1.returncode
    
    logging.info(f"frontend : {frontend_exit_code}")
    
    backend_exit_code:int = p2.returncode
    
    logging.info(f"backend :  {backend_exit_code}")
    

if __name__ == "__main__":
    main()