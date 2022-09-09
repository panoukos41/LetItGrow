# NODE SPECIFICATION

This document contains the specs needed for the application to run on a node.

The connection pins are selected for a `Raspberry Pi Zero W` you can configure them in the `appsettings.yaml` file.

# Appsettings.yaml

- ### ClientId
    `The id of the node`
- ### Token
    `Token that will be send to the server`
- #### Server
    `Url to the server that this node should connect`
- ### Certificate
    `Path to certificate`
- ### LED
    - `Power` = `05` Green when app is running otherwise off.
        > GREEN WIRE
    - `Connection` = `06` Red when app is not connected otherwise off.
        > WHITE WIRE
- ### DHT
    - `Pin` = `` The pin the sensor is connected to.
        >
    - `Led` = `13` On when app is using the sensor otherwise off.
        > YELLOW WIRE
    - `Version` = `11` or `12` or `21` or `22`
- ### SOIL
    - `Pin` = `` The pin the sensor is connected to.
        >
    - `Led` = `19` On when app is using the sensor otherwise off.
        > BROWN WIRE
- ### PUMP
    - `Pin` = `` The pin the actuator is connected to.
        >
    - `Led` = `26` On when app is using the actuator otherwise off.
        > BLUE WIRE