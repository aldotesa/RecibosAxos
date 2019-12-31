using RecibosAxosPersistence.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecibosAxosPersistence.Context.Mappings
{
    public class ProvedorConfiguration : EntityTypeConfiguration<Provedor>
    {
        public ProvedorConfiguration()
        {
            ToTable("Provedor");
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Nombre).HasMaxLength(100).IsRequired();
            HasMany(x => x.Recibos).WithRequired(x => x.Provedor).HasForeignKey(x => x.ProvedorId);
        }
    }
}
