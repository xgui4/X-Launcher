import subprocess
import os

from hatchling.builders.hooks.plugin.interface import BuildHookInterface

class CustomBuildHook(BuildHookInterface):
    def initialize(self, version, build_data):
        if os.name == "posix":
            print("Linux setups scripts coming later")
        if os.name == "nt":
            print("Windows setups scripts coming later")
        return super().initialize(version, build_data)