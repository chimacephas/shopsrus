using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopsRUs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            var customer = new List<Customer>
            {
                new Customer
                {
                    Id = Guid.NewGuid(),
                    IsAnAffliate = true,
                    Name = "John",
                    CreatedAt = DateTime.Now

                },
                new Customer
                {
                    Id = Guid.NewGuid(),
                    IsAnEmployee = true,
                    Name = "Alice",
                    CreatedAt = DateTime.Now

                },
                new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "Doe",
                    CreatedAt = DateTime.Now
                },
                new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "Max",
                    CreatedAt = DateTime.Now.AddYears(-3)
                }
            };

            builder.HasData(customer);
        }
    }
}
