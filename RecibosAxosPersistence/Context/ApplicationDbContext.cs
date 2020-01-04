using Microsoft.AspNet.Identity.EntityFramework;
using RecibosAxosPersistence.Context.Mappings;
using RecibosAxosPersistence.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecibosAxosPersistence.Context
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(): base("DefaultConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>();
            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUsers");
            modelBuilder.Entity<ApplicationUser>().HasMany(x=>x.Recibos).WithRequired(x=>x.Usuario).HasForeignKey(x=>x.IdUsuario);
            modelBuilder.Entity<IdentityRole>();
            modelBuilder.Entity<IdentityUserRole>();
            modelBuilder.Entity<IdentityUserClaim>();
            modelBuilder.Entity<IdentityUserLogin>();
            modelBuilder.Configurations.Add(new ReciboConfiguration());
            modelBuilder.Configurations.Add(new ProvedorConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
