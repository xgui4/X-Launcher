#!/usr/bin/env bash

PROJECT_ROOT="${1:-../}"

rm -rf "$PROJECT_ROOT/target/"*

find "$PROJECT_ROOT" -name "__pycache__" -type d -delete