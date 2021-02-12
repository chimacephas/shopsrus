using Moq;
using ShopsRUs.Application.Common.Repository.Interface;
using ShopsRUs.Application.Common.Utils;
using ShopsRUs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Tests.Mocks
{
    public class DiscountRepositoryMock
    {
        public static Mock<IDiscountRepository> GetDiscountRepository()
        {
            var discounts = new List<Discount>
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

            var mockeddiscountRepository = new Mock<IDiscountRepository>();
            mockeddiscountRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(discounts);

            mockeddiscountRepository.Setup(x => x.GetAllAsync(It.IsAny<Expression<Func<Discount, bool>>>()))
           .ReturnsAsync((Expression<Func<Discount, bool>> func) =>
           {
               return discounts.Where(func.Compile()).ToList();
           });

            mockeddiscountRepository.Setup(x => x.Add(It.IsAny<Discount>()));

            mockeddiscountRepository.Setup(x => x.DoesExist(It.IsAny<Expression<Func<Discount, bool>>>()))
              .Returns((Expression<Func<Discount, bool>> func) =>
              {
                  return discounts.Any(func.Compile());
              });

            mockeddiscountRepository.Setup(x => x.SingleOrDefaultAsync(It.IsAny<Expression<Func<Discount, bool>>>()))
           .ReturnsAsync((Expression<Func<Discount, bool>> func) =>
           {
               return discounts.Where(func.Compile()).FirstOrDefault();
           });

            return mockeddiscountRepository;
        }
    }
}
