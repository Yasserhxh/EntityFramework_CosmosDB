using Domain.Authentication;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.Json;

namespace Repository.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {

        }
         public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Declaration> declarations { get; set; }
        public DbSet<Intervention> Interventions { get; set; }
        private static string ToJson<T>(T item) => JsonSerializer.Serialize(item);
        private static T FromJson<T>(IList<Intervention> json) => JsonSerializer.Deserialize<T>(json.ToString());
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Declaration>().
                ToContainer("Declaration")
                .HasKey(d=>d.Dclaration_ID);
           
            builder.Entity<Intervention>()
                .ToContainer("Declaration")
                .HasKey(i => i.Intervention_ID);
            builder.Entity<ApplicationUser>().ToContainer("ApplicationUser").Property(d => d.ConcurrencyStamp).IsETagConcurrency();
            builder.Entity<IdentityRole>().ToContainer("IdentityRole").Property(d => d.ConcurrencyStamp).IsETagConcurrency();
            
          
            builder.Entity<Declaration>().
               ToContainer("Declaration").HasPartitionKey(d => d.Dclaration_ID);
            builder.Entity<Intervention>().
             ToContainer("Intervention").HasPartitionKey(d => d.Intervention_ID);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseCosmos(
                "https://oneeleaksdb.documents.azure.com:443/",
                "mq44w6HocomOqReFwJ1mpRFS1yY4iu1Y4BUiAt383Ae9uhYiLpOYJS8tVEwKf74oy3nYDybHbcjbjXCYc0UH2Q==",
                databaseName: "OneeLeaks");

    }
}
