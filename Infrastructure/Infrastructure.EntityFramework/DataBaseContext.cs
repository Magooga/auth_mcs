using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Domain.Entities;
using System.Data;
using System.Reflection.Emit;
using Microsoft.Extensions.Logging;

namespace Infrastructure.EntityFramework
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; } // добавил надо ли это для третей таблицы??

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
            .Entity<User>()
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<UserRole>(
            j => j
                .HasOne(pt => pt.Role)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(pt => pt.Role_Id),  // связь с таблицей Roles через Role_Id
            j => j
                .HasOne(pt => pt.User)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(pt => pt.User_Id), // связь с таблицей Users через User_Id
            j =>
            {
                j.Property(pt => pt.CreateDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
                j.HasKey(t => new { t.User_Id, t.Role_Id, t.Id });
                j.ToTable("UserRoles"); // Имя таблицы
            });
         }
    }
}