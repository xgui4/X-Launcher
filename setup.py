#!/usr/bin/env python
import os
import subprocess

print("Do you already have task file installed ?")



if (os.name == "poisx"):
    subprocess.call("sh" , "-c", "\"$(curl --location https://taskfile.dev/install.sh)\"", "--" "-d-")
    
else:
    subprocess.call("pip", "install", "go-task-bin")

print("Do you already have UV installed ?")    

if (os.name == "poisx"):
    subprocess.call("curl", "-LsSf", "https://astral.sh/uv/install.sh", "|", "sh")
    
else:
    subprocess.call("pip", "install", "uv")