using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoyalTea_Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.DataAccess.Configurations
{
    public class CountrySpecification : EntityConfiguration<Country>
    {
        protected override void ConfigureConstraints(EntityTypeBuilder<Country> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
