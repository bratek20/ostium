#!/bin/bash

cd ..

./gradlew clean build -x test

cp web-app/build/libs/web-app.jar bash/server/

