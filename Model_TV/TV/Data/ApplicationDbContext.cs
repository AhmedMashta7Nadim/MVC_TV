using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model_TV.Models;
using System.Reflection.Emit;
using TV.Models;

namespace TV.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserName>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Attachment> Attachment { get; set; } 
        public DbSet<TV_Show> TV_Show { get; set; }
        public DbSet<Languages> Languages { get; set; }
        public DbSet<TV_ShowLanguages> TV_Languages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Languages>().HasData(
                new Languages()
                {
                    Id = Guid.Parse("75f63810-bfaa-451e-9d2f-d5af64b50f2b"),
                    Name="English",
                },
                 new Languages()
                 {
                     Id = Guid.Parse("3ea19e0b-f99e-4393-8d82-0b5d13eec785"),
                     Name = "Arabic"
                 }
             );


            base.OnModelCreating(builder);

            builder.Entity<TV_Show>()
                   .Property(t => t.IsActive)
                   .HasDefaultValue(true);

            builder.Entity<TV_Show>()
                .HasMany(x => x.languages)
                .WithMany(x => x.tV_Shows)
                .UsingEntity<TV_ShowLanguages>(
                x =>
                {
                    x.HasOne(x => x.TV_Show)
                .WithMany(x => x.tv_languages)
                .HasForeignKey(x => x.Id);

                    x.HasOne(x => x.Languages)
                        .WithMany(x => x.tv_languages)
                        .HasForeignKey(x => x.Id);
                });

            builder.Entity<TV_ShowLanguages>()
                   .HasOne(x => x.TV_Show)
                   .WithMany(x => x.tv_languages)
                   .HasForeignKey(x => x.TV_ShowId);

            builder.Entity<TV_ShowLanguages>()
                   .HasOne(x => x.Languages)
                   .WithMany(x => x.tv_languages)
                   .HasForeignKey(x => x.LanguagesId);

        }
    }
}
