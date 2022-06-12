using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoyalTea_Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.DataAccess.Configurations
{
    public class CurrencyConfiguration : EntityConfiguration<Currency>
    {
        protected override void ConfigureConstraints(EntityTypeBuilder<Currency> builder)
        {
            builder.Property(x => x.IsoCode).HasMaxLength(5).IsRequired();
        }
    }
}
