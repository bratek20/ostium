#!/bin/bash

moduleGroup=$1
profileName=$2
moduleName=$3

hlaFolderPath=src/$moduleGroup

java -jar tool.jar update $hlaFolderPath $profileName $moduleName

