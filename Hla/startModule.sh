#!/bin/bash

hlaFolderPath=src
profileName=$1
moduleName=$2

java -jar tool.jar start $hlaFolderPath $profileName $moduleName

