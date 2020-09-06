using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    //Specify ApplicationUser class as the generic argument for the IdentityDbContext class
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {           
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();

            //In Entity Framework Core, by default the foreign keys in AspNetUserRoles table have Cascade 
            //DELETE behaviour. This means, if a record in the parent table (AspNetRoles) is deleted, then the 
            //corresponding records in the child table (AspNetUserRoles ) are automatically be deleted.

            //Following lines of code are to not allow a role to be deleted, if there are rows in the child table 
            //(AspNetUserRoles) which point to a role in the parent table (AspNetRoles).
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
