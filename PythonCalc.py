import math


def crestron_main(module_info_object):
    if module_info_object.args[0] == "Divide":
        module_info_object.set(str(float(module_info_object.args[1])/float(module_info_object.args[2])))
        return

    if module_info_object.args[0] == "Multiply":
        module_info_object.set(str(float(module_info_object.args[1])*float(module_info_object.args[2])))
        return

    if module_info_object.args[0] == "Add":
        module_info_object.set(str(float(module_info_object.args[1])+float(module_info_object.args[2])))
        return

    if module_info_object.args[0] == "Subtract":
        module_info_object.set(str(float(module_info_object.args[1])-float(module_info_object.args[2])))
        return

    if module_info_object.args[0] == "Power":
        module_info_object.set(str(math.pow(float(module_info_object.args[1]), float(module_info_object.args[2]))))
        return

    module_info_object.set(str("hmm"))
