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
    public class OrderStatusConfiguration : EntityConfiguration<OrderStatus>
    {
        protected override void ConfigureConstraints(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(20).IsRequired();

            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany(x => x.Orders).WithOne(x => x.OrderStatus).HasForeignKey(x => x.OrderStatusId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
