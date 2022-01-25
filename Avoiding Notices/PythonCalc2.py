import sys
import time
import json


def data_received_callback(twentytwentyone):
    print("PYTHON: Printing from simpltest.py subscribe() callback: " + twentytwentyone)
    jsstring = json.loads(twentytwentyone)
    if twentytwentyone["type"] == "add":
        print("yay, it was add")


def crestron_main(module_info_object):
    print("PYTHON: reached crestron_main")
    print("PYTHON: " + repr(module_info_object))

    my_guid = module_info_object.uid
    args = module_info_object.args

    print("PYTHON: guid = " + repr(my_guid) + " args = " + repr(args))

    module_info_object.set("sent from simpltest.py crestron_main")

    module_info_object.subscribe(data_received_callback)

    time.sleep(5)
    module_info_object.set("sent from simpltest.py after 5 seconds")
    time.sleep(100)
    module_info_object.set("sent from simpltest.py after 10 seconds")


def main():
    print("PYTHON: Hello from simpltest.py")
    print("PYTHON: Argument list: " + str(sys.argv))


if __name__ == "__main__":
    main()

if __name__ == "simpltest":
    main()

print("PYTHON: __name__ = " + __name__)
