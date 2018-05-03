using Microsoft.EntityFrameworkCore;
using BookCave.Data.EntityModels;

namespace BookCave.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(
                 "Server=tcp:verklegt2.database.windows.net,1433;Initial Catalog=VLN2_2018_H08;Persist Security Info=False;User ID=VLN2_2018_H08_usr;Password=hug3Home66;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}