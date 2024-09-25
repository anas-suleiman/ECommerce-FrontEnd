# E-Commerce Microservices Platform - Communication Patterns Evaluation
## Overview
This project is a simulated implementation of an e-commerce microservices platform. The purpose of the implementation is to evaluate the performance, scalability, complexity, and reliability of different communication patterns such as REST APIs, gRPC, and RabbitMQ. The system includes core services: Order Management Service, Payment Service, Inventory Service, and Notification Service. The case study focuses on the communication between these services rather than the internal business logic, transactions, or rollback mechanisms.

## System Architecture
The e-commerce platform simulates communication between microservices using different patterns, focusing on:\
Order Management Service: Manages order creation.\
Payment Service: Handles payment processing.\
Inventory Service: Updates stock levels.\
Notification Service: Sends email notifications to users based on order status and other events.

## Technology Stack
Programming Language: C# (ASP.NET Core)\
Communication Patterns:\
REST API: For synchronous HTTP communication.\
gRPC: For high-performance synchronous communication.\
RabbitMQ: For asynchronous communication using message queues.\
Databases: In-Memory Database (for simplicity in the simulation)\
Logging: Integrated logging with built-in .NET logging libraries.\


## Communication Patterns
The platform is implemented using three communication patterns:

REST API: Based on ASP.NET Core Web API. This pattern is used to simulate synchronous communication between services.
gRPC: Based on ASP.NET Core gRPC. This pattern provides high-performance communication using HTTP/2.
RabbitMQ: Asynchronous communication using a message broker, allowing services to communicate through message queues.

## Installation and Setup
Prerequisites
.NET 8 SDK \
RabbitMQ Server installed and running locally\
gRPC tools for .NET

## Solution Structure
FrontEnd folder contains client projects as follows:\
FrontEnd for RESTAPI\
GRPCFrontEndClient for gRPC\
RabbitMQFrontEndClient for RabbitMQ

Shared folder contains shared models.

Services folder contains the projects for each service as follows:\
Inventory REST API: InventoryCore, InventorySDK and InventoryRESTAPI\
Inventory gRPC: InventoryRPC\
Inventory RabbitMQ: InventoryRabbitMQ

Notification REST API: NotificationCore, NotificationSDK and NotificationRESTAPI\
Notification gRPC: NotificationRPC\
Notification RabbitMQ: NotificationRabbitMQ

OrderManagement REST API: OrderManagementCore, OrderManagementSDK and OrderManagementRESTAPI\
OrderManagement gRPC: OrderManagementRPC\
OrderManagement RabbitMQ: OrderManagementRabbitMQ

Payment REST API: PaymentCore, PaymentSDK and PaymentRESTAPI\
Payment gRPC: PaymentRPC\
Payment RabbitMQ: PaymentRabbitMQ

## Port number distribution
| Project Name				| Port Number |\
| FrontEnd					| 7087        |\
| InventoryRESTAPI			| 7048        |\
| NotificationRESTAPI		| 7215        |\
| OrderManagementRESTAPI	| 7138        |\
| PaymentRESTAPI			| 7177        |\
| InventoryRPC				| 7285        |\
| NotificationRPC			| 7108        |\
| OrderManagementRPC		| 7224        |\
| PaymentRPC				| 7046        |


## How to Run
To run the solution different combination of projects should be selected as startup projects.\
For RESTAPI the following projects are required:\
1- FrontEnd\
2- InventoryRESTAPI\
3- NotificationRESTAPI\
4- OrderManagementRESTAPI\
5- PaymentRESTAPI

For gRPC the following projects are required:\
1- GRPCFrontEndClient\
2- InventoryRPC\
3- NotificationRPC\
4- OrderManagementRPC\
5- PaymentRPC

For RabbitMQ the following projects are required:\
1- RabbitMQFrontEndClient\
2- InventoryRabbitMQ\
3- NotificationRabbitMQ\
4- OrderManagementRabbitMQ\
5- PaymentRabbitMQ
