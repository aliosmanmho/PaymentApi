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
    public class BinNumberConfiguration : IEntityTypeConfiguration<BinNumber>
    {
        public void Configure(EntityTypeBuilder<BinNumber> builder)
        {

            builder.ToTable("BinNumbers");
            builder.HasKey(e => e.BinCode);
            builder.Property(e => e.CartType).IsRequired(true).HasMaxLength(1);
            builder.Property(e => e.BankName).IsRequired(true).HasMaxLength(100);
            builder.Property(e => e.Organization).IsRequired(true).HasMaxLength(20);
        }
    }
}
