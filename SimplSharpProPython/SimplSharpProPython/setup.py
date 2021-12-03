import sys

def crestron_main(module_info_object):
    ModulePath = "/user/PythonDependencies"
    
    if ModulePath not in sys.path:
        sys.path.append(ModulePath)