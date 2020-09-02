using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    //Your application DbContext class must inherit from IdentityDbContext class instead of DbContext class. 
    //This is required because IdentityDbContext provides all the DbSet properties needed to manage the identity
    //tables in SQL Server.
    public class AppDbContext : IdentityDbContext
    {
        //DbContextOptions in Entity Framework Core
        //For the DbContext class to be able to do any useful work, it needs an instance of the DbContextOptions class.
        //The DbContextOptions instance carries configuration information such as the connection string, database provider
        //to use etc.
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        //Entity Framework Core DbSet
        //The DbContext class includes a DbSet<TEntity> property for each entity in the model.
        //At the moment in our application we have, only one entity class - Employee.
        //So in our AppDbContext class we only have one DbSet<Employee> property.
        //We will use this DbSet property Employees to query and save instances of the Employee class.
        //The LINQ queries against the DbSet<TEntity> will be translated into queries against the underlying database.
        public DbSet<Employee> Employees { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Keys of Identity tables are mapped in OnModelCreating method of IdentityDbContext class. 
            //So, to fix the error, 'This migration contains code that creates the tables required by the ASP.NET Core 
            //Identity system.Error: The entity type 'IdentityUserLogin<string>' requires a primary key to be defined'
            //Call the base class OnModelCreating() method using the base keyword as shown below.
            base.OnModelCreating(modelBuilder);

            modelBuilder.Seed();           
        }
    }
}
