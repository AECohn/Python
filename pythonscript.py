import sys
import time


def data_received_callback(json):
    print("PYTHON: Printing from simpltest.py subscribe() callback: " + json)


def crestron_main(module_info_object):
    print("PYTHON: reached crestron_main")
    print("PYTHON: " + repr(module_info_object))

    my_guid = module_info_object.uid
    args = module_info_object.args

    print("PYTHON: guid = " + repr(my_guid) + " args = " + repr(args))

    module_info_object.set("sent from simpltest.py crestron_main")

    module_info_object.subscribe(data_received_callback)

    time.sleep(5)
    module_info_object.set("Hello" + module_info_object.args[0])
    time.sleep(5)
    module_info_object.set("Hello" + module_info_object.args[1])


def main():
    print("PYTHON: Hello from simpltest.py")
    print("PYTHON: Argument list: " + str(sys.argv))


if __name__ == "__main__":
    main()

if __name__ == "simpltest":
    main()

print("PYTHON: __name__ = " + __name__)
