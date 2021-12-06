import sys
import time




def crestron_main(module_info_object):

    module_info_object.set("Hello, " + module_info_object.args[0])
    time.sleep(1)
    module_info_object.set("Hello, " + module_info_object.args[1])



