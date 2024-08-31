#!/bin/bash

SERVER_URL=$1

cp ../../web-app-tester/build/libs/web-app-tester.jar .

java -jar web-app-tester.jar $SERVER_URL

