using AutoMapper;
using FluentAssertions;
using Moq;
using ShopsRUs.Application.Common.Models;
using ShopsRUs.Application.Common.Repository.Interface;
using ShopsRUs.Application.Features.Customers.Models;
using ShopsRUs.Application.Features.Customers.Queries;
using ShopsRUs.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ShopsRUs.Tests.Customers.Queries
{
    public class CustomerQueryTests
    {

        private readonly Mock<ICustomerRepository> _customerRepository;
        private readonly IMapper _mapper;
        public CustomerQueryTests()
        {
            _customerRepository = CustomerRepositoryMock.GetCustomerRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }


        [Fact]
        public async Task GetAllCustomers_ReturnsAllCustomers()
        {
            var handler = new GetAllCustomerQueryHandler(_customerRepository.Object, _mapper);

            var result = await handler.Handle(new GetAllCustomerQuery(), CancellationToken.None);

            result.Should().HaveCount(4);
        }

        [Fact]
        public async Task GetCustomerById_ValidId_ReturnsCustomer()
        {
            var id = Guid.Parse("B0788D2F-8003-43C1-92A4-EDC76A7C5DDE");

            var handler = new GetCustomerByIdQueryHandler(_customerRepository.Object, _mapper);

            var result = await handler.Handle(new GetCustomerByIdQuery(id), CancellationToken.None);

            var matchedResult = result.Match<CustomerDto>(x => x,x => new CustomerDto());

            matchedResult.Id.Should().Be(id);
        }


        [Fact]
        public async Task GetCustomerById_InValidId_ReturnsNotFound()
        {
            var id = Guid.Parse("B0788D2F-8003-43C1-92A4-EDC76A7C5DDD");

            var handler = new GetCustomerByIdQueryHandler(_customerRepository.Object, _mapper);

            var result = await handler.Handle(new GetCustomerByIdQuery(id), CancellationToken.None);

            var matchedResult = result.Match<NotFound>(x => null, x => x);

            matchedResult.Description.Should().Be("Customer does not exist");
        }

        [Fact]
        public async Task GetCustomerByName_ValidName_ReturnsCustomer()
        {
            var name = "John";

            var handler = new GetCustomerByNameQueryHandler(_customerRepository.Object, _mapper);

            var result = await handler.Handle(new GetCustomerByNameQuery(name), CancellationToken.None);

            var matchedResult = result.Match<CustomerDto>(x => x, x => new CustomerDto());

            matchedResult.Name.Should().Be(name);
        }


        [Fact]
        public async Task GetCustomerByName_InValidName_ReturnsNotFound()
        {
            var name = "Kate";

            var handler = new GetCustomerByNameQueryHandler(_customerRepository.Object, _mapper);

            var result = await handler.Handle(new GetCustomerByNameQuery(name), CancellationToken.None);

            var matchedResult = result.Match<NotFound>(x => null, x => x);

            matchedResult.Description.Should().Be("Customer does not exist");
        }
    }
}
