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


____________________________________________
در زمینه سیستم های صف پیام، به ویژه با کتابخانه هایی مانند RabbitMQ که از پروتکل AMQP استفاده می کنند، channel.BasicAckAsync یک روش مربوط به تأیید پیام است.

هدف BasicAckAsync
تأیید ناهمزمان: BasicAckAsync برای تأیید دریافت پیام از صف پیام استفاده می شود. هنگامی که یک مصرف کننده پیامی را دریافت و پردازش می کند، یک تأییدیه را برای کارگزار ارسال می کند تا نشان دهد که پیام با موفقیت پردازش شده است. این کار از ارسال مجدد پیام جلوگیری می کند.

پارامترها:

deliveryTag: این یک شناسه منحصر به فرد برای پیامی است که مصرف کننده دریافت کرده است. کارگزار از آن برای ردیابی تصدیق استفاده می کند.
multiple: یک پرچم بولی که در صورت درست بودن، همه پیام ها را تا تگ تحویل مشخص شده تایید می کند. اگر نادرست باشد، فقط پیام خاص مربوط به برچسب تحویل تایید می شود.
--------------------------------------------------------------------
در سیستم‌های پیام‌رسانی مانند RabbitMQ که از پروتکل AMQP استفاده می‌کنند، روش channel.BasicNackAsync برای مدیریت پیام‌های تایید منفی استفاده می‌شود. در اینجا یک نمای کلی از هدف، پارامترها و نحوه انطباق آن با پردازش پیام آورده شده است.

هدف BasicNackAsync
تأیید منفی: روش BasicNackAsync زمانی فراخوانی می شود که مصرف کننده نتواند یک پیام را با موفقیت پردازش کند. به کارگزار پیام اطلاع می دهد که پیام (یا پیام ها) خاص به درستی پردازش نشده است و می تواند:
Requeue the message: به این معنی است که پیام برای پردازش مجدد توسط همان مصرف کننده یا مصرف کننده دیگر در صف قرار می گیرد.
رد کردن پیام: بسته به پیکربندی کارگزار و پارامترهای استفاده شده، پیام را می توان به جای درخواست نادیده گرفت.
پارامترها
پارامترهایی که معمولاً با BasicNackAsync درگیر هستند در اینجا آمده است:

deliveryTag: یک شناسه منحصر به فرد برای پیامی که تایید نشده است. این تگ به RabbitMQ اجازه می دهد تا تشخیص دهد که کدام پیام به طور منفی تایید شده است.

multiple: یک پرچم بولی که رفتار تایید را نشان می دهد:

اگر درست باشد: همه پیام‌هایی که برچسب‌های تحویل برابر یا کمتر از تحویل مشخص شده دارند، به‌طور منفی تایید می‌شوند.
اگر نادرست است: فقط پیام مشخص شده به صورت منفی تایید می شود.
Requeue: یک پرچم بولی که تعیین می کند آیا پیام باید در نوبت قرار گیرد یا خیر:

اگر درست است: پیام برای پردازش مجدد در صف قرار می گیرد.
اگر نادرست است: پیام حذف می شود و برای هیچ مصرف کننده ای ارسال نمی شود.
--------------------------------------------------------------------
در سیستم‌های پیام‌رسانی مانند RabbitMQ که از پروتکل AMQP استفاده می‌کنند، روش channel.BasicConsumeAsync نقش مهمی در مصرف پیام‌ها از یک صف دارد. در اینجا به تفکیک هدف و کاربرد آن اشاره شده است.

هدف BasicConsumeAsync
مصرف پیام ناهمزمان: BasicConsumeAsync به این معنی است که یک مصرف کننده برای دریافت پیام ها به صورت ناهمزمان در صفی مشترک می شود. این بدان معنی است که مصرف کننده می تواند پیام ها را بدون مسدود کردن رشته اصلی پردازش کند.

راه‌اندازی مصرف‌کننده: وقتی با BasicConsumeAsync تماس می‌گیرید، یک شنونده را در یک صف مشخص تنظیم می‌کنید. هنگامی که پیام ها در آن صف می رسند، مصرف کننده به طور خودکار آنها را بر اساس روش پاسخ به تماس ارائه شده دریافت می کند.

پارامترها
در اینجا پارامترهای معمولی درگیر با BasicConsumeAsync آمده است:

queue: نام صفی که پیام ها از آن مصرف می شود.
autoAck: یک پرچم بولی. اگر درست باشد، پیام ها به صورت خودکار پس از دریافت تایید می شوند. اگر نادرست است، مصرف کننده باید صریحاً هر پیام را تأیید کند (اغلب از BasicAckAsync استفاده می کند).
تگ مصرف کننده: یک شناسه اختیاری برای مصرف کننده. این می تواند به مدیریت چندین مصرف کننده کمک کند.
noLocal: یک بولی که تعیین می کند آیا مصرف کننده باید فقط پیام های منتشر شده توسط سایر اتصالات را دریافت کند. اگر درست باشد، پیام‌های منتشر شده توسط همان اتصال تحویل داده نمی‌شوند.
منحصر به فرد: نشان می دهد که آیا مصرف کننده منحصر به اتصال است یا خیر. اگر درست باشد، فقط این مصرف کننده می تواند پیام ها را از صف مصرف کند.
آرگومان ها: آرگومان های اختیاری اضافی برای تنظیم مصرف کننده.