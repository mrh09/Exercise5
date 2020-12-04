using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using API.Models;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext() : base("MyConnection") { }

        public DbSet<Supplier> Suppliers { get; set; }
    }
}