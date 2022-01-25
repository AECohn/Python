import math
import json
import time


def data_received_callback(jonah):
    print("PYTHON: Printing from simpltest.py subscribe() callback: " + jonah)
    jsstring = json.loads(jonah)
    print(jsstring)
    

def add (values):
    return float(values[1]) + float(values[2])

def subtract (values):
    return float(values[1]) - float(values[2])

def divide (values):
    return float(values[1]) / float(values[2])

def multiply (values):
    return float(values[1]) * float(values[2])

def power (values):
    return math.pow(float(values[1]), float(values[2]))

def crestron_main(module_info_object):
    while(True):
        module_info_object.subscribe(data_received_callback)
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
        time.sleep(0.01)
   