using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using transitProject.Models;

namespace transitProject.DAL
{
    public class TransitContext : DbContext
    {
        public TransitContext() : base("TransitContext")
        {

        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Stop> Stops { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Event> Events { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
        }

    }
}