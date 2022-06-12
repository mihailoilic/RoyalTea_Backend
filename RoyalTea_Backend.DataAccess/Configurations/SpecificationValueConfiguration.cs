using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoyalTea_Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.DataAccess.Configurations
{
    public class SpecificationValueConfiguration : EntityConfiguration<SpecificationValue>
    {
        protected override void ConfigureConstraints(EntityTypeBuilder<SpecificationValue> builder)
        {
            builder.Property(x => x.Value).HasMaxLength(30).IsRequired();
            builder.HasIndex(x => x.Value).IsUnique();

            builder.HasMany(x => x.ProductSpecificationValues).WithOne(x => x.SpecificationValue).HasForeignKey(x => x.SpecificationValueId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
