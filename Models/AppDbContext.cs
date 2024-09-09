using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity; // DbContext

namespace WebAppMuffins.Models
{
    //2
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("MUffinsDB") { }

        public DbSet<Orders> orders { get; set; }
        public DbSet<MuffinItems> muffinItems { get; set; }
    }
}