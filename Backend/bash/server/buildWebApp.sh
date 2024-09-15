#!/bin/bash

cd ..

./gradlew clean build

cp web-app/build/libs/web-app.jar bash/server/

