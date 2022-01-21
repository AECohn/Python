import math


def crestron_main(module_info_object):
    result = ''

    if module_info_object.args[0] == "Divide":
        result = float(module_info_object.args[1])/float(module_info_object.args[2])

    elif module_info_object.args[0] == "Multiply":
        result = float(module_info_object.args[1]) * float(module_info_object.args[2])

    elif module_info_object.args[0] == "Add":
        result = float(module_info_object.args[1]) + float(module_info_object.args[2])

    elif module_info_object.args[0] == "Subtract":
        result = float(module_info_object.args[1]) - float(module_info_object.args[2])

    elif module_info_object.args[0] == "Power":
        result = math.pow(float(module_info_object.args[1]), float(module_info_object.args[2]))

    else:
        module_info_object.set(str("Command not found"))

    answer = str(result)
    if answer.endswith('.0'):
        answer = answer.replace('.0', 'hmm')

    module_info_object.set(answer)
