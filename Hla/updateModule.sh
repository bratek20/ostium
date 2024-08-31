#!/bin/bash

hlaFolderPath=src
profileName=$1
moduleName=$2

java -jar tool.jar update $hlaFolderPath $profileName $moduleName

