using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FSDRealEstate.Models;
using System.Xml;

namespace FSDRealEstate.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Property> Property { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<FSDRealEstate.Models.Image> Image { get; set; }



        
    }
}
