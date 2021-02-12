using Moq;
using ShopsRUs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopsRUs.Application.Common.Repository.Interface;
using System.Linq.Expressions;

namespace ShopsRUs.Tests.Mocks
{
    public class CustomerRepositoryMock
    {
        public static Mock<ICustomerRepository> GetCustomerRepository()
        {
            var customers = new List<Customer>
            {
                new Customer
                {
                    Id = Guid.Parse("B0788D2F-8003-43C1-92A4-EDC76A7C5DDE"),
                    IsAnAffliate = true,
                    Name = "John",
                    CreatedAt = DateTime.Now

                },
                new Customer
                {
                    Id = Guid.Parse("6313179F-7837-473A-A4D5-A5571B43E6A6"),
                    IsAnEmployee = true,
                    Name = "Alice",
                    CreatedAt = DateTime.Now

                },
                new Customer
                {
                    Id = Guid.Parse("BF3F3002-7E53-441E-8B76-F6280BE284AA"),
                    Name = "Doe",
                    CreatedAt = DateTime.Now
                },
                new Customer
                {
                    Id = Guid.Parse("FE98F549-E790-4E9F-AA16-18C2292A2EE9"),
                    Name = "Max",
                    CreatedAt = DateTime.Now.AddYears(-3)
                }
            };


            var mockedcustomerRepository = new Mock<ICustomerRepository>();
            mockedcustomerRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(customers);

            mockedcustomerRepository.Setup(x => x.Add(It.IsAny<Customer>()));

            mockedcustomerRepository.Setup(x => x.DoesExist(It.IsAny<Expression<Func<Customer, bool>>>()))
            .Returns((Expression<Func<Customer, bool>> func) =>
            {
                return customers.Any(func.Compile());
            });

            mockedcustomerRepository.Setup(x => x.SingleOrDefaultAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
           .ReturnsAsync((Expression<Func<Customer, bool>> func) =>
           {
               return customers.Where(func.Compile()).FirstOrDefault();
           });




            return mockedcustomerRepository;
        }
    }
}
