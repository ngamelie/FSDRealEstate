using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FSDRealEstate.Models;

namespace FSDRealEstate.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Property> Property { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}
