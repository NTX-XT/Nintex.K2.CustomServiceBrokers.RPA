# Nintex RPA Service Broker
 __Static services broker to allow  Nintex K2 to trigger Nintex RPA Robots by creating a task and assigning it to a queue__

Note that the  current service broker works only for K2 Five (on-premises or hosted installation) and doesn't work for K2 Cloud, a sepearate broker compitable with Nintex K2 Cloud will be created shortley.

## Tech-Stack
- Dotnet Framework v4.7 
- C#
- GraphQL Client 

## Features
- Get List of Nintex RPA Queues
- Create Task And Assign in to Sepecific Nintex RPA Queue

## Dependecies
Nintex RPA Service Broker relies on the below Nuget Packages to facilitate calling GraphQL API 
- [GraphQL](https://graphql.org/graphql-js/graphql-clients/) client to execute calls to Nintex RPA public API
- [NetwonSoft.Json](nuget.org/packages/Newtonsoft.Json/) to Serialize /Deserialize the GraphQL API calls results in a strongly typed objects

In addition to the above packages, Nintex RPA Service Broker relies on the below extenisability DLL:
- ServiceSDK : SourceCode.SmartObjects.Services.ServiceSDK.dll , extensibility SDK to create Nintex K2 Five custom service brokers.

## Installation
 Installation is striaght forward, first you need to install all the dependecies to Global Assembly Cache **GAC**, strong named &signed assembleies of the dependenceis has been packaged and pushed as part of the repository, Creating an installer is something we are working on at the moment.
 to install dependecies manually now you need to copy the strong named assemblies then use gacutil to install them one by one
```sh
cd StrongNamedDLLs
gacutil -i GraphQL.Client.dll
gacutil -i GraphQL.Client.Serializer.Newtonsoft.dll
gacutil -i NewtonSoft.Json.dll
gacutil -i GraphQL.Client.Abstractions.dll
gacutil -i GraphQL.Client.Abstractions.Websocket.dll
gacutil -i GraphQL.Primitives.dll
```
then you can copy the output DLL **Nintex.Emerging.Integration.dll**  to your K2 Five Servers

## Service Broker in Action

Once you have deployed the service broker you will be able to:
1- Create a new Service Type using the deployed DLL
2- Create New Service instance in this step you would need to add your Nintex RPA server parameters
	- URL
	- Scheme (http vs https) we strongly recommend using https and configure your Nintex RPA public API endpoint using the same.
	- UserName (user that has access to consume the public API)
	- Password
3- Once the Service instance has been created you can then generate the smart objects that you can use from your workflow
4- At the time of writing this readme.md we have implemented two methods only 
	- Create Task  (used to add a task with predefined set of parameters and a set of dynamic parameters passed in key, value tuples to the Nintex RPA public API.
	- List Queues  (list all queues which comes in handy to assign task to specific queue


**_Important Note_**

You can use the Nintex RPA broker in two different modes:
- Fire and forget : triggering an RPA bot to execute sepcific wizard 
- Fire and wait for a response: You can do this by passing the Nintex K2 Wait for external system checkbox and pass the workflow activity SerialNo , this will make your Nintex Workflow wait for the RPA bot to call back and resume the workflow 
it will also allow it to pass some outcomes back to the Nintex K2 workflow.
	
![Nintex RPA](https://github.com/NTX-XT/Nintex.K2.CustomServiceBrokers.RPA/blob/f1bb7a81be062e9c2925e7a6f4914a2c1090b310/Add_RPA_Task.png?raw=true)
## Development

Want to contribute? Great you will need 
- Microsoft Visual studio
- Nintex K2 environment (Development)
- Nintex RPA environment

## License

MIT
