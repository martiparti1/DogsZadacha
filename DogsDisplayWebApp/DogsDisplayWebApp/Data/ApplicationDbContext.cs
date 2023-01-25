using DogsDisplayWebApp.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using DogsDisplayWebApp.Models;

namespace DogsDisplayWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<DogsDisplayWebApp.Models.DogCreateViewModel> DogCreateViewModel { get; set; }
        public DbSet<DogsDisplayWebApp.Models.DogDetailsViewModel> DogDetailsViewModel { get; set; }
    }
}
