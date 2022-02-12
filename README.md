# iot-platform-sensor-data
This repository includes applications that communicate with sensor open data APIs in order to request, receive, display and analyze environmental data from various sensors and locations.<br><br>
In order to collect the data from the sensors the following are used:<br>
- API:  https://github.com/opendata-stuttgart/meta/wiki/EN-APIs<br>
- Maps: https://maps.sensor.community/

## ./node-red
Inside node-red subfolder you can find the implementation of the specifications by using the node-red framework. Please read `/node-red/Documentation.pdf` for more information.<br>If you wish to setup the project locally, please follow the steps described inside `/node-red/installation-Guide.pdf`.

## ./c#-win-service
Windows Service to collect the sensor data, store them in a database and provide statistics based on country and city.
