using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Context
{
    public class ObrasContext : DbContext
    {
        public DbSet<Author> Author { get; set; }

        public ObrasContext(DbContextOptions<ObrasContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
