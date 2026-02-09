#!/usr/bin/env bash

dotnet build 'src/backend/X_Launcher.Core/X_Launcher.Core.csproj' -c Release -o 'target/backend/X_Launcher.Core' -f net9.0

dotnet build 'src/backend/X_Launcher.Service/X_Launcher.Service.csproj' -c Release -o 'target/backend/X_Launcher.Service' -f net9.0

cp 'src/x-launcher-core.py' target/x-launcher-core.py

mkdir -p target/frontend

cp -r src/frontend/*  target/frontend