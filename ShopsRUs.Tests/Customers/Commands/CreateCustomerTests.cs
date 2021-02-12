using AutoMapper;
using FluentAssertions;
using Moq;
using ShopsRUs.Application.Common.Repository;
using ShopsRUs.Application.Common.Repository.Interface;
using ShopsRUs.Application.Features.Customers.Commands;
using ShopsRUs.Application.Features.Customers.Models;
using ShopsRUs.Domain.Entities;
using ShopsRUs.Tests.Mocks;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ShopsRUs.Tests.Customers.Commands
{
    public class CreateCustomerTests
    {
        private readonly Mock<ICustomerRepository> _customerRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly IMapper _mapper;
        public CreateCustomerTests()
        {
            _customerRepository = CustomerRepositoryMock.GetCustomerRepository();
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidCustomer_AddsCustomerToRepo()
        {
            var handler = new CreateCustomerCommandHandler(_customerRepository.Object, _unitOfWork.Object, _mapper);

           var result = await handler.Handle(new CreateCustomerCommand { Name = "Chima" }, CancellationToken.None);


            _customerRepository.Verify(x => x.Add(It.IsAny<Customer>()),Times.Once);
            _unitOfWork.Verify(x => x.SaveChangesAsync(),Times.Once);
            result.Should().BeOfType<CustomerDto>();
        }


    }
}
