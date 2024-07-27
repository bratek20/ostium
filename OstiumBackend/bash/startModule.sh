#!/bin/bash

hlaFolderPath=../hla
profileName=lib
moduleName=$1

java -jar tool.jar start $hlaFolderPath $profileName $moduleName

