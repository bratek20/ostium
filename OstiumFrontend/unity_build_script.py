import subprocess

EDITOR_PATH = "/home/mateusz/Unity/Hub/Editor/2022.3.37f1/Editor/Unity"
PROJECT_PATH = "/home/mateusz/work/ostium/OstiumFrontend/OstiumView"
CONFIG_PATH = "/home/mateusz/work/ostium/OstiumFrontend/build_config.json"

def run(cmd):
    process = subprocess.Popen(cmd, shell=True, stdout=subprocess.PIPE)
    while process.stdout.readable():
        line = process.stdout.readline()
        if not line:
            break

        formatted_line = str(line.strip(), 'utf-8')
        print(formatted_line)

    process.wait()
    return process.returncode

def build():
    cmd = f'{EDITOR_PATH} '\
                '-batchmode '\
                '-nographics '\
                '-quit'\
                '-disable-assembly-updater '\
                f'-projectPath "{PROJECT_PATH}" '\
                f'-buildTarget "android" '\
                '-logFile - '\
                f'-executeMethod BuildTools.BuildRunner.Build '\
                f'-config {CONFIG_PATH}'
                
    result = run(cmd)
    print(result)

build()