using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkListAPI.Auth;

namespace WorkListAPI.Models
{
    public class ApplicationDbContext: IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<ListItem> ListItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ListItem>(entity =>
            {
                entity.Property(e => e.ListItemName)
                .IsRequired()
                .HasMaxLength(20);

                entity.Property(e => e.ListItemDesc)
                .IsRequired()
                .HasMaxLength(150);

                entity.Property(e => e.ListItemStatus)
                .IsRequired()
                .HasMaxLength(1);

            });
            base.OnModelCreating(builder);
        }
    }
}
