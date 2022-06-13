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
    public class OrderConfiguration : EntityConfiguration<Order>
    {
        protected override void ConfigureConstraints(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.IsCancelled).HasDefaultValue(false);
            builder.Property(x => x.OrderStatusId).HasDefaultValue(1);

            builder.HasMany(x => x.OrderItems).WithOne(x => x.Order).HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
