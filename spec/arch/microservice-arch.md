# MICROSERVICE



## REST Server

The first codeblock is for the HTTP method and the second code block is for the SignalR method.

### Nodes

#### Search all nodes
```http
  GET /v1/node
```
```http
  node:search
```
| Parameter | Type            | Return          | Description |
| :-------- | :-------------- | :-------------- | :---------- |
| `request` | [SearchNodes]() | [NodeModel]()[] | A search query          |

#### Get node
```http
  GET /v1/node/{id}
```
```http
  node:get
```
| Parameter | Type     | Return        | Description |
| :-------- | :------- | :------------ | :---------- |
| `request` | `string` | [NodeModel]() | **Required**. The node's id |

#### Create node
```http
  POST /v1/node
```
```http
  node:create
```
| Parameter | Type           | Return        | Description |
| :-------- | :------------- | :------------ | :---------- |
| `request` | [CreateNode]() | [NodeModel]() | **Required**. Create a new node |

#### Update node
```http
  PATCH /v1/node
```
```http
  node:update
```
| Parameter | Type           | Return          | Description |
| :-------- | :------------- | :-------------- | :---------- |
| `request` | [UpdateNode]() | [ModelUpdate]() | **Required**. Update an existing node |

#### Delete node
```http
  DELETE /v1/node/{id}
```
```http
  node:delete
```
| Parameter | Type     | Return   | Description |
| :-------- | :------- | :------- | :---------- |
| `request` | `string` | [Unit]() | **Required**. The node's id |

#### Add to group
```http
  PATCH /v1/node/add
```
```http
  node:group-add
```
| Parameter | Type         | Return          | Description |
| :-------- | :----------- | :-------------- | :---------- |
| `request` | [GroupAdd]() | [ModelUpdate]() | **Required**. Add a node to a group |

#### Remove from group
```http
  PATCH /v1/node/remove
```
```http
  node:group-remove
```
| Parameter | Type            | Return          | Description |
| :-------- | :-------------- | :-------------- | :---------- |
| `request` | [GroupRemove]() | [ModelUpdate]() | **Required**. Remove a nodes group |

#### Refresh token
```http
  PATCH /v1/node/refresh
```
```http
  node:refresh
```
| Parameter | Type             | Return          | Description |
| :-------- | :--------------- | :-------------- | :---------- |
| `request` | [RefreshToken]() | [RefreshModel]() | **Required**. Refresh a nodes token |

#### Node Added (Client notification SignalR only)
```http
  node:added
```
| Type            | Description |
| :-------------- | :---------- |
| [NodeCreated]() | Notification received when node is created. |

#### Node Updated (Client notification SignalR only)
```http
  node:updated
```
| Type            | Description |
| :-------------- | :---------- |
| [NodeUpdated]() | Notification received when node is updated. |

#### Node Removed (Client notification SignalR only)
```http
  node:removed
```
| Type            | Description |
| :-------------- | :---------- |
| [NodeDeleted]() | Notification received when a node is deleted. |

#### Node Connection (Client notification SignalR only)
```http
  	
```
| Type               | Description |
| :----------------- | :---------- |
| [NodeConnection]() | Notification received when a connection is updated. |

### Groups

#### Search all groups
```http
  GET /v1/group
```
```http
  group:search
```
| Parameter | Type             | Return           | Description |
| :-------- | :--------------- | :--------------- | :---------- |
| `request` | [SearchGroups]() | [GroupModel]()[] | A search query          |

#### Get group
```http
  GET /v1/group/{id}
```
```http
  group:get
```
| Parameter | Type     | Return         | Description |
| :-------- | :------- | :------------- | :---------- |
| `request` | `string` | [GroupModel]() | **Required**. The group's id |

#### Create group
```http
  POST /v1/group
```
```http
  group:create
```
| Parameter | Type            | Return         | Description |
| :-------- | :-------------- | :------------- | :---------- |
| `request` | [CreateGroup]() | [GroupModel]() | **Required**. Create a new group |

#### Update group
```http
  PATCH /v1/group/
```
```http
  group:update
```
| Parameter | Type            | Return          | Description |
| :-------- | :-------------- | :-------------- | :---------- |
| `request` | [UpdateGroup]() | [ModelUpdate]() | **Required**. Update an existing group |

#### Delete group
```http
  DELETE /v1/group/{id}
```
```http
  group:delete
```
| Parameter | Type     | Return   | Description |
| :-------- | :------- | :------- | :---------- |
| `request` | `string` | [Unit]() | **Required**. The group's id |

#### Group Added (Client notification SignalR only)
```http
  group:added
```
| Type             | Description |
| :--------------- | :---------- |
| [GroupCreated]() | Notification received when a group is created. |

#### Group Updated (Client notification SignalR only)
```http
  group:updated
```
| Type             | Description |
| :--------------- | :---------- |
| [GroupUpdated]() | Notification received when a group is updated. |

#### Group Removed (Client notification SignalR only)
```http
  group:removed
```
| Type             | Description |
| :--------------- | :---------- |
| [GroupDeleted]() | Notification received when a group is deleted. |

### Irrigations

#### Search irrigations
```http
  GET /v1/irrigation
```
```http
  irrigation:search
```
| Parameter | Type                  | Return                | Description |
| :-------- | :-------------------- | :-------------------- | :---------- |
| `request` | [SearchIrrigations]() | [IrrigationModel]()[] | **Required** A search query |

#### Search many irrigations
```http
  GET /v1/irrigation/many
```
```http
  irrigation:search-many
```
| Parameter | Type                  | Return                | Description |
| :-------- | :-------------------- | :-------------------- | :---------- |
| `request` | [SearchManyIrrigations]() | [IrrigationModel]()[] | **Required** A search query |

### Measurements

#### Search measurements
```http
  GET /v1/measurement
```
```http
  measurement:search
```
| Parameter | Type                   | Return                 | Description |
| :-------- | :--------------------- | :--------------------- | :---------- |
| `request` | [SearchMeasurements]() | [MeasurementModel]()[] | **Required** A search query |

#### Search many measurements
```http
  GET /v1/measurement/many
```
```http
  measurement:search-many
```
| Parameter | Type                   | Return                 | Description |
| :-------- | :--------------------- | :--------------------- | :---------- |
| `request` | [SearchManyMeasurements]() | [MeasurementModel]()[] | **Required** A search query |

## MQTT Server

#### HTTP Get connected nodes
```http
  GET /connected
```
| Parameters         | Type     | Return     | Description |
| :----------------- | :------- | :--------- | :---------- |
| `user`, `password` | `string` | `string[]` | Returns the connected node ids |

#### Node connection topic
```http
  node/connection/{nodeId}"
```
| Payload Type       | Description |
| :----------------- | :---------- |
| [NodeConnection]() | The connection status of a node |

#### Node settings topic
```http
  node/settings/{nodeId}
```
| Payload Type                                      | Description |
| :------------------------------------------------ | :---------- |
| [IrrigationSettings]() or [MeasurementSettings]() | A node's settings |

#### Node irrigation topic
```http
  node/irrigation/{nodeId}
```
| Payload Type         | Description |
| :------------------- | :---------- |
| [CreateIrrigation]() | A new irrigation |

#### Node measurement topic
```http
  node/measurement/{nodeId}
```
| Payload Type          | Description |
| :-------------------- | :---------- |
| [CreateMeasurement]() | A new measurement |
