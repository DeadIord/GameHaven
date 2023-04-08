using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Reflection.Emit;
using Web_Application.Models;


namespace Web_Application.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Computers> Computers { get; set; }
        public DbSet<Halls> Halls { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<Visiting> Visiting { get; set; }
        public DbSet<Visitor> Visitors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            


            base.OnModelCreating(builder);// Задает схему для базы данных
            builder.HasDefaultSchema("Identity");   /* Переименовывает таблицу пользователей из AspNetUsers в Identity.User. */
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User");
            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

           
            builder.Entity<Services>()
                 .HasMany(h => h.Visitings)
                 .WithOne(v => v.Services)
                 .HasForeignKey(v => v.ServicesCode);
         

            builder.Entity<Visitor>()
                            .HasOne(v => v.Visiting)
                            .WithOne(v => v.Visitor)
                            .HasForeignKey<Visiting>(v => v.VisitorCode);

            builder.Entity<Halls>()
       .HasMany(h => h.Visitings)
       .WithOne(v => v.Halls)
       .HasForeignKey(v => v.HallsCode)
       .OnDelete((DeleteBehavior)ReferentialAction.NoAction);

            builder.Entity<Visiting>()
            .HasIndex(v => new { v.HallsCode, v.ComputersId, v.DateOfVisit })
            .IsUnique();


            builder.Entity<Computers>()
                              .HasOne(c => c.Halls)
                              .WithMany(h => h.Computers)
                              .HasForeignKey(c => c.HallsCode);

            builder.Entity<Visiting>()
                            .HasOne(v => v.ApplicationUser)
                            .WithMany(au => au.Visitings)
                            .HasForeignKey(v => v.ApplicationUserId);

           
           

        }
    }

}
