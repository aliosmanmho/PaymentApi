using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Data.Context.Configurations
{
    internal class PaymentActivityConfiguration : IEntityTypeConfiguration<PaymentActivity>
    {
        public void Configure(EntityTypeBuilder<PaymentActivity> builder)
        {
            builder.ToTable("PaymentActivities");
            builder.Property(e => e.Id).UseSerialColumn();
            builder.Property(e => e.Amount).IsRequired(true).HasColumnType("decimal(18,2)");
            builder.Property(e => e.Status).IsRequired(true).HasDefaultValue(1);
            builder.Property(e => e.CreateDate).HasColumnType("timestamp without time zone").HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
        }
    }
}
