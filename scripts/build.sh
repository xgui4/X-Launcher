#!/usr/bin/env bash

PROJECT_ROOT="${1:-../}"

/usr/lib/qt6/rcc -g python "$PROJECT_ROOT/data/ressources.qrc" -o "$PROJECT_ROOT/src/frontend/ressources_rc.py" 

/usr/lib/qt6/uic -g python "$PROJECT_ROOT/launcher.ui" -o "$PROJECT_ROOT/src/frontend/launcher_ui.py"

dotnet build "$PROJECT_ROOT/src/backend/X_Launcher.Core/X_Launcher.Core.csproj" -c Release -o "$PROJECT_ROOT/target/backend/X_Launcher.Core" -f net9.0

dotnet build "$PROJECT_ROOT/src/backend/X_Launcher.Service/X_Launcher.Service.csproj" -c Release -o "$PROJECT_ROOT/target/backend/X_Launcher.Service" -f net9.0

cp "$PROJECT_ROOT/src/x-launcher-core.py" "$PROJECT_ROOT/target/x-launcher-core.py"

mkdir -p "$PROJECT_ROOT/target/frontend"

cp -r "$PROJECT_ROOT"/src/frontend/* "$PROJECT_ROOT/target/frontend/"