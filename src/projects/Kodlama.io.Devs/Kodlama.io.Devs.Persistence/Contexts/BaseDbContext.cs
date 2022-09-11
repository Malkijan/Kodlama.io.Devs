using Core.Security.Entities;
using Kodlama.io.Devs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        public IConfiguration Configuration { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<Tech> Techs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //    base.OnConfiguring(
            //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SomeConnectionString")));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgrammingLanguage>(pl =>
            {
                pl.ToTable("ProgrammingLanguages").HasKey(k => k.Id);
                pl.Property(p => p.Id).HasColumnName("Id");
                pl.Property(p => p.Name).HasColumnName("Name");
                pl.HasMany(p => p.Techs);
            });

            modelBuilder.Entity<Tech>(tech =>
            {
                tech.ToTable("Techs").HasKey(k => k.Id);
                tech.Property(p => p.Id).HasColumnName("Id");
                tech.Property(p => p.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId");
                tech.Property(p => p.Name).HasColumnName("Name");
                
                tech.HasOne(p => p.ProgrammingLanguage);
            });

            modelBuilder.Entity<User>(user =>
            {
                user.ToTable("Users").HasKey(k => k.Id);
                user.Property(p => p.Id).HasColumnName("Id");
                user.Property(p => p.FirstName).HasColumnName("FirstName");
                user.Property(p => p.LastName).HasColumnName("LastName");
                user.Property(p => p.Email).HasColumnName("Email");
                user.Property(p => p.PasswordSalt).HasColumnName("PasswordSalt");
                user.Property(p => p.PasswordHash).HasColumnName("PasswordHash");
                user.Property(p => p.Status).HasColumnName("Status");
                user.Property(p => p.AuthenticatorType).HasColumnName("AuthenticatorType");
                
                user.HasMany(p => p.RefreshTokens);
                user.HasMany(p => p.UserOperationClaims);

            });

            modelBuilder.Entity<OperationClaim>(oc =>
            {
                oc.ToTable("OperationClaims").HasKey(k => k.Id);
                oc.Property(p => p.Id).HasColumnName("Id");
                oc.Property(p => p.Name).HasColumnName("Name");
            });

            modelBuilder.Entity<UserOperationClaim>(uoc =>
            {
                uoc.ToTable("UserOperationClaims").HasKey(k => k.Id);
                uoc.Property(p => p.Id).HasColumnName("Id");
                uoc.Property(p => p.UserId).HasColumnName("UserId");
                uoc.Property(p => p.OperationClaimId).HasColumnName("OperationClaimId");
            });

            modelBuilder.Entity<Social>(s =>
            {
                s.ToTable("Socials").HasKey(k => k.Id);
                s.Property(p => p.Id).HasColumnName("Id");
                s.Property(p => p.UserId).HasColumnName("UserId");
                s.Property(p => p.SocialUrl).HasColumnName("SocialUrl");

                s.HasOne(p => p.User);
                
            });

            modelBuilder.Entity<Tech>().Navigation(x => x.ProgrammingLanguage).AutoInclude();
            modelBuilder.Entity<ProgrammingLanguage>().Navigation(x => x.Techs).AutoInclude();
            modelBuilder.Entity<Social>().Navigation(x => x.User).AutoInclude();

            ProgrammingLanguage[] programmingLanguageSeedData = { new(1, "c#"), new(2, "Java") };
            modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguageSeedData);

            Tech[] techSeedData = { new(1,1,"WPF"),new(2,1,"ASP.NET"), new(3, 3, "Spring"), new(4, 3, "JSP") };
            modelBuilder.Entity<Tech>().HasData(techSeedData);

            OperationClaim[] claims = { new(1, "Admin"), new(2, "User") };
            modelBuilder.Entity<OperationClaim>().HasData(claims);
        }
    }
}
