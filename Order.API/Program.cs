
using Microsoft.Extensions.Options;
using Order.API.Data;
using Order.API.Messaging;
using Order.API.Repository;
using Order.API.Services;

namespace Order.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

 
            builder.Services.AddControllers();
             builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddDbContext<AppDbContext>();
            //builder.Services.AddDbContext<AppDbContext>(options =>
            //{
              //  options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
            //});

            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddSingleton<ISenderMessage, SenderMessage>();
          

            var app = builder.Build();
            //var dbContext = app.Services.GetRequiredService<AppDbContext>();
            //dbContext.Database.EnsureCreated();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}