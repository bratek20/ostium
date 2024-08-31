#!/bin/bash

hlaFolderPath=src
profileName=$1

java -jar tool.jar updateAll $hlaFolderPath $profileName

