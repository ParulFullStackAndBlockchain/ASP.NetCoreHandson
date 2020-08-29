using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class AppDbContext : DbContext
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
    }
}
