1- add 3 console (one sender and tow receiver)
2- Install-Package RabbitMQ.Client -Version 7.0.0 | Install-Package System.Text.Json -Version 9.0.0
3- docker pull focker.ir/rabbitmq:3-management
4- docker tag focker.ir/rabbitmq:3-management rabbitmq:3-management
5- docker image ls
6- docker run -d --hostname my-rabbitmq --name rabbitmq-home -p 8080:15672 -p 5672:5672 rabbitmq:3-management
7- http://localhost:8080/ =>guest:guest

*************Sender***************
1- create connection
2-create channel 
	2-1- ExchangeDeclareAsync
	2-2- QueueDeclareAsync 
	2-3- QueueBindAsync
	2-4- BasicPublishAsync => byte[] mesaage
 3-channel => CloseAsync
 4-connection => CloseAsync
 ************Receiver**************
1- create connection
2-create channel 
	2-1- ExchangeDeclareAsync
	2-2- QueueDeclareAsync 
	2-3- QueueBindAsync
	2-4- BasicQosAsync 
	2-5- AsyncEventingBasicConsumer
	2-6- BasicConsumeAsync
	2-7- BasicCancelAsync
 3-channel => CloseAsync
 4-connection => CloseAsync

 https://www.tutlane.com/tutorial/rabbitmq/csharp-rabbitmq-direct-exchange