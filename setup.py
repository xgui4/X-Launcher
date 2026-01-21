#!/usr/bin/env python
import os
import subprocess

print("Do you already have task file installed ?")

if os.name == "poisx":
    print(
        subprocess.run(
            [
                "sh",
                "-c",
                '"$(curl --location https://taskfile.dev/install.sh)"',
                "--",
                "-d-",
            ]
        )
    )

else:
    print(subprocess.run(["pip", "install", "go-task-bin"]))

print("Do you already have UV installed ?")

if os.name == "poisx":
    print(
        subprocess.run(["curl", "-LsSf", "https://astral.sh/uv/install.sh", "|", "sh"])
    )
else:
    print(subprocess.run(["pip", "install", "uv"]))
