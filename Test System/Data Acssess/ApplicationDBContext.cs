using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using Test_System.Migrations;
using Test_System.Models;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Test_System.Data_Acssess
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Category> categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductColor> productColors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSubImage> ProductSubImages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source = DESKTOP-EEU926S; Integrated Security = True;" +
                " Initial Catalog = EE_Commerce; Connect Timeout = 30; Encrypt = True; Trust Server Certificate=True;" +
                " Application Intent = ReadWrite; Multi Subnet Failover=False");
        }
    }
}










