#!/bin/bash

moduleGroup=$1
profileName=$2

hlaFolderPath=src/$moduleGroup

java -jar tool.jar updateAll $hlaFolderPath $profileName

