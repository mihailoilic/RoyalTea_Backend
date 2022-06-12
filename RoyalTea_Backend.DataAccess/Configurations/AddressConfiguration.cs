using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoyalTea_Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.DataAccess.Configurations
{
    public class AddressConfiguration : EntityConfiguration<Address>
    {
        protected override void ConfigureConstraints(EntityTypeBuilder<Address> builder)
        {
            builder.Property(x => x.DeliveryLocation).HasMaxLength(200).IsRequired();
            
            
        }
    }
}
