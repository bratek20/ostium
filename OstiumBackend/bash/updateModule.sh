#!/bin/bash

hlaFolderPath=../hla
profileName=lib
moduleName=$1

java -jar tool.jar update $hlaFolderPath $profileName $moduleName

