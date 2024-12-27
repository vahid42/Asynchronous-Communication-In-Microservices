Add-Migration InitialCreate
Update-Database 

3- when INotificationService inject to MessageConsumerService  is problem 
    An error occurred while accessing the Microsoft.Extensions.Hosting services. Continuing without the application service provider. Error: Some services are not able to be constructed (Error while validating the service descriptor 'ServiceType: Microsoft.Extensions.Hosting.IHostedService Lifetime: Singleton ImplementationType: Notification.Api.Messaging.MessageConsumerService': Cannot consume scoped service 'Notification.API.Services.INotificationService' from singleton 'Microsoft.Extensions.Hosting.IHostedService'.)
4-Solution Use a Factory:If you need to keep MessageConsumerService as a singleton, consider using a factory pattern or a service locator to resolve the scoped service when needed:
csharp
 private readonly IServiceProvider _serviceProvider;  

    public MessageConsumerService(IServiceProvider serviceProvider)  
    {  
        _serviceProvider = serviceProvider;  
    }  

    public async Task StartAsync(CancellationToken cancellationToken)  
    {  
        using (var scope = _serviceProvider.CreateScope())  
        {  
            var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();  
            // Use notificationService here  
        }  
    }  
}  
