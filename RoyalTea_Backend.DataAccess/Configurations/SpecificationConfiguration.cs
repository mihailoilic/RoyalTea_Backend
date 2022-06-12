using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoyalTea_Backend.Domain;

namespace RoyalTea_Backend.DataAccess.Configurations
{
    public class SpecificationConfiguration : EntityConfiguration<Specification>
    { 

        protected override void ConfigureConstraints(EntityTypeBuilder<Specification> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany(x => x.SpecificationValues).WithOne(x => x.Specification).HasForeignKey(x => x.SpecificationId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.CategorySpecifications).WithOne(x => x.Specification).HasForeignKey(x => x.SpecificationId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
