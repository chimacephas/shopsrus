using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopsRUs.Application.Common.Utils;
using ShopsRUs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Persistence.Configurations
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            var discount = new List<Discount>
            {
              new Discount
              {
                  Id = Guid.NewGuid(),
                  Percentage = 10,
                  Name = "Affliate Discount",
                  Type = Constants.AffliateDiscount,
                  CreatedAt = DateTime.Now
              },
              new Discount
              {
                  Id = Guid.NewGuid(),
                  Percentage = 30,
                  Name = "Employee Discount",
                  Type = Constants.EmployeeDiscount,
                  CreatedAt = DateTime.Now
              },
              new Discount
              {
                  Id = Guid.NewGuid(),
                  Percentage = 5,
                  Name = "Old Customer Discount",
                  Type = Constants.OldCustomerDiscount,
                  CreatedAt = DateTime.Now
              },
              new Discount
              {
                  Id = Guid.NewGuid(),
                  Percentage = 5,
                  Name = "Percent Per $100 Bill Discount",
                  Type = Constants.PercentPer100DollarDiscount,
                  CreatedAt = DateTime.Now
              }
            };

            builder.HasData(discount);
        }
    }
}
