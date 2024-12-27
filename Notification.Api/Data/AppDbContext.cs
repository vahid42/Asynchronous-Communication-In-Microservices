using Microsoft.EntityFrameworkCore;

namespace Notification.API.Data
{
    public class AppDbContext : DbContext
    {


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //"C:\\Users\\Laptop-V\\AppData\\Local\\Order.db"
            this.DbPath = Init();
        }
        private string DbPath { get; }
        private static string Init()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            return System.IO.Path.Join(path, "NotificationDB.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            //readme.txt
        }

        public DbSet<Entities.Notification> Notifications { get; set; }



    }
}
