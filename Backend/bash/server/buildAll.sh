#!/bin/bash

cd ../..

./gradlew clean build

cp web-app/build/libs/web-app.jar bash/server/
cp web-app-tester/build/libs/web-app-tester.jar bash/server/

