import subprocess
import argparse

# simple usage: python3 ./unity_build_script.py --path "View/test.apk" --distribute --notes "automated release"

EDITOR_PATH = "/home/mateusz/Unity/Hub/Editor/2022.3.37f1/Editor/Unity"
PROJECT_PATH = "/home/mateusz/work/ostium/Frontend/View"
CONFIG_PATH = "/home/mateusz/work/ostium/Frontend/build_config.json"

parser = argparse.ArgumentParser()

parser.add_argument('--path', type=str,
                    help='path to app')

parser.add_argument('--distribute', action='store_true',
                    help='should distribute to firebase app distribution')

parser.add_argument('--notes', type=str,
                    help='release notes', 
                    default="release")

args = parser.parse_args()

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
                
    return run(cmd)

def distribute():
    cmd = f'firebase appdistribution:distribute '\
                f'{args.path} '\
                '--app 1:265791525210:android:a25a61182ea3ed859b2363 '\
                f'--release-notes "{args.notes}" '\
                '--testers "bartoszrudzkilo14@gmail.com, mateuszka94@gmail.com"'
    result = run(cmd)
    print(result)

result = build()
if args.distribute and result == 0:
    distribute()