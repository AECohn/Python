import json
import time
import sys
import requests
import os


def process_url(urlLink,run_object):
    success_flag = False
    try : 
        response = requests.get(urlLink)
        pageContent = response.text
        file_path = os.getcwd()
        file = os.path.basename(os.path.normpath(urlLink.strip()))
        filename = file_path +'/'+ file +'.html'
        with open(filename,'w',encoding="utf-8") as f:
            f.write(pageContent)
            f.close()
        run_object.module_info.set(json.dumps({'file_path':filename,"uid":run_object.module_info.uid}), saved=True)  
        success_flag = True  
    except requests.ConnectionError as e:
        print(e.response.text)
    finally:
        return success_flag

class run_class:

    def __init__(self, module_info):
        self.run = True        
        self.module_info = module_info
        self.status = "NotStarted"

    def set_run(self, val):
        self.run = val

    def get_run(self):
        return self.run
    
    def get_status(self):
        return self.status

    def set_status(self,val):
        self.status = str(val)

def wrapper(run_object):

    def run_callback(val):
        jsonData = json.loads(val)
        
        print(f"Python module {run_object.module_info.uid} in program {run_object.module_info.args[0]} received {str(jsonData)}")              

        if 'Running' in jsonData:
            if jsonData['Running'] == "False":
                print("Exiting Module")
                run_object.set_run(False)

        if 'URL' in jsonData:
            url_link = jsonData['URL']   
            run_object.set_status("InProgress")
            run_object.module_info.set(json.dumps({'file_status':run_object.get_status(),"uid":run_object.module_info.uid}), saved=True)

            if process_url(str(url_link).strip(),run_object) :
                run_object.set_status("Completed")
                run_object.module_info.set(json.dumps({'file_status':run_object.get_status(),"uid":run_object.module_info.uid}), saved=True)
            else:
                print ("URL does not exist on Internet. Enter a vaid URL.")
                run_object.set_status("Error : Enter a valid URL")
                run_object.module_info.set(json.dumps({'file_status':run_object.get_status(),"uid":run_object.module_info.uid}), saved=True)
    return run_callback

def crestron_main(module_info_object):
    my_guid = module_info_object.uid
    args = module_info_object.args[0].strip()
    r = run_class(module_info_object)
   
    module_info_object.subscribe(wrapper(r))
    
    try:
        if not len(args) == 0:
            if process_url(args,r):
                r.set_status("Completed")
                module_info_object.set(json.dumps({'file_status':r.get_status(),"uid":r.module_info.uid}), saved=True)
            else:
                 print ("URL does not exist on Internet. Enter a vaid URL.")
                 r.set_status("Error : Enter a valid URL")
                 module_info_object.set(json.dumps({'file_status':r.get_status(),"uid":r.module_info.uid}), saved=True)

            r.set_run(False)
            module_info_object.set(json.dumps({'Running':r.get_run(),"uid":module_info_object.uid}), saved=True)
        else:
            module_info_object.set(json.dumps({'file_status':r.get_status(),"uid":module_info_object.uid}), saved=True)
        
        while r.get_run():
            continue

    except Exception as e:
        print(e, file=sys.stderr)
