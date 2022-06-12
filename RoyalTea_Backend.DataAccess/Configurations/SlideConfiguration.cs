using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoyalTea_Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.DataAccess.Configurations
{
    public class SlideConfiguration : EntityConfiguration<Slide>
    {
        protected override void ConfigureConstraints(EntityTypeBuilder<Slide> builder)
        {
            builder.Property(x => x.HtmlContent).IsRequired();
        }
    }
}
