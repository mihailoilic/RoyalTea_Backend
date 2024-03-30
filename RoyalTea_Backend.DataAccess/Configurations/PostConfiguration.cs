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
    public class PostConfiguration : EntityConfiguration<Post>
    {
        protected override void ConfigureConstraints(EntityTypeBuilder<Post> builder)
        {
            builder.Property(x => x.Title).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.Views).IsRequired().HasDefaultValue(0);
        }
    }
}
