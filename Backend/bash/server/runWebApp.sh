#!/bin/bash

cd server

java -Xmx256m -Xms256m -XX:+UseG1GC -XX:+UseStringDeduplication -XX:+ParallelRefProcEnabled -jar web-app.jar

