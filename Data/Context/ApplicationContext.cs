using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Conversion> Conversion { get; set; }
        public DbSet<Subscription> Subscription { get; set; }

    }
}
