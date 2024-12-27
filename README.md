The goal of this project is to demonstrate how to communicate asynchronously between microservices:
This project has two parts:
	The first part is a console implementation
	It includes a sender and two message receivers.
The second part includes two microservices(Order.Api and Notification.Api) written in ASP.Net, each with a separate database
The first microservice, called Order.Api, creates orders and provides them to the message broker. The second microservice Notification.Api receives them and stores them in the database, assuming that an email is sent
In these projects:
.Net Core 7
Asp.net Core
RabbitMQ.Client (In Console Projects)
AutoMapper
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Sqlite
Microsoft.EntityFrameworkCore.Tools
