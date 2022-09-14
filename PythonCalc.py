import math
def add (values):
    try:
        return float(values[1]) + float(values[2])
    except Exception as err:
        return err
def subtract (values):
    try:
        return float(values[1]) - float(values[2])
    except Exception as err:
        return err
def divide (values):
    try:
        return float(values[1]) / float(values[2])
    except Exception as err:
        return err
def multiply (values):
    try:
        return float(values[1]) * float(values[2])
    except Exception as err:
        return err
def power (values):
    try:
        return math.pow(float(values[1]), float(values[2]))
    except Exception as err:
        return err
def crestron_main(module_info_object):
    if module_info_object.args[0] == 'Divide':
        result = divide(module_info_object.args)
    elif module_info_object.args[0] == 'Multiply':
        result = multiply(module_info_object.args)
    elif module_info_object.args[0] == 'Add':
        result = add(module_info_object.args)
    elif module_info_object.args[0] == 'Subtract':
        result = subtract(module_info_object.args)
    elif module_info_object.args[0] == 'Power':
        result = power(module_info_object.args)
    else:
        module_info_object.set(str('Command not found'))
    answer = str(result)
    if answer.endswith('.0'):
        answer = answer.replace('.0', '')
    module_info_object.set(answer)
