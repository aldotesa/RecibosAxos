﻿using RecibosAxosPersistence.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecibosAxosPersistence.Context.Mappings
{
    public class ReciboConfiguration : EntityTypeConfiguration<Recibo>
    {
        public ReciboConfiguration()
        {
            ToTable("Recibo");
            HasKey(x => x.IdRecibo);
            Property(x => x.IdRecibo).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Monto).IsRequired();
            Property(x => x.Fecha).IsRequired();
            Property(x=>x.Moneda).IsRequired();
            Property(x=>x.Comentario).IsOptional();
            HasRequired(x => x.Provedor).WithMany(x=>x.Recibos).HasForeignKey(x=>x.IdProvedor);
            HasRequired(x => x.Usuario).WithMany(x => x.Recibos).HasForeignKey(x=>x.IdUsuario);
        }
    }
}
