using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Payment.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Data
{
    public class PaymentContextFactory : IDesignTimeDbContextFactory<PaymentContext>
    {
        public PaymentContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PaymentContext>();
            optionsBuilder.UseNpgsql("Host=ec2-34-254-120-2.eu-west-1.compute.amazonaws.com;Database=d736mbo2dppptg;Username=rpjcnydbmlhkjz;Password=8dcaf55a9ab4c10215df7b504ef0d50e94801ac856f851c585bbef597f0260ec;SSL Mode=Require;Trust Server Certificate=true");

            return new PaymentContext(optionsBuilder.Options);
        }
    }
}
