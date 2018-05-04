using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookCave.Data;
using BookCave.Data.EntityModels;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookCave
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            //SeedData();
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        public static void SeedData()
        {
            var db = new DataContext();
            
            if(!db.Books.Any())
            {
                var initialBooks = new List<Book>()
                {
                    new Book { Title = "Grey Sister"},
                    new Book { Title = "The Neon Boneyard"}
                };
                db.AddRange(initialBooks);
                db.SaveChanges();
            }
        }
    }
}
