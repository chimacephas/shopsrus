using FluentAssertions;
using Moq;
using ShopsRUs.Application.Common.Models;
using ShopsRUs.Application.Common.Repository;
using ShopsRUs.Application.Common.Repository.Interface;
using ShopsRUs.Application.Features.Invoices.Models;
using ShopsRUs.Application.Features.Invoices.Queries;
using ShopsRUs.Domain.Entities;
using ShopsRUs.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ShopsRUs.Tests.Invoices
{
    public class GetInvoiceAmountTests
    {

        private readonly Mock<ICustomerRepository> _customerRepository;
        private readonly Mock<IDiscountRepository> _discountRepository;
        private readonly Mock<IInvoiceRepository> _inventorypository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        public GetInvoiceAmountTests()
        {
            _customerRepository = CustomerRepositoryMock.GetCustomerRepository();
            _discountRepository = DiscountRepositoryMock.GetDiscountRepository();
            _inventorypository = new Mock<IInvoiceRepository>();
            _inventorypository.Setup(x => x.Add(It.IsAny<Invoice>()));
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);
        }


        [Fact]
        public async Task Handle_NonExistingCustomer_ReturnNotFound()
        {
            var id = Guid.Parse("B0788D2F-8003-43C1-92A4-EDC76A7C5DDD");

            var handler = new GetTotalInvoiceAmountQueryHandler(_inventorypository.Object,_customerRepository.Object,
                _discountRepository.Object, _unitOfWork.Object);

            var result = await handler.Handle(new GetTotalInvoiceAmountQuery
            {
               IsGrocery = false,
                Amount = 300,
                 CustomerId = id
            }, CancellationToken.None);

            var matchedResult = result.Match<NotFound>(x => null, x => x);
            matchedResult.Description.Should().Be("Customer does not exist");
        }


        [Fact]
        public async Task Handle_AffliateCustomer_ReturnDiscount()
        {
            var id = Guid.Parse("B0788D2F-8003-43C1-92A4-EDC76A7C5DDE");

            var handler = new GetTotalInvoiceAmountQueryHandler(_inventorypository.Object, _customerRepository.Object,
                _discountRepository.Object, _unitOfWork.Object);

            var result = await handler.Handle(new GetTotalInvoiceAmountQuery
            {
                IsGrocery = false,
                Amount = 300,
                CustomerId = id
            }, CancellationToken.None);

            var matchedResult = result.Match<InvoiceDto>(x => x, x => null);

            matchedResult.Amount.Should().Be(270);
        }


        [Fact]
        public async Task Handle_EmployeeCustomer_ReturnDiscount()
        {
            var id = Guid.Parse("6313179F-7837-473A-A4D5-A5571B43E6A6");

            var handler = new GetTotalInvoiceAmountQueryHandler(_inventorypository.Object, _customerRepository.Object,
                _discountRepository.Object, _unitOfWork.Object);

            var result = await handler.Handle(new GetTotalInvoiceAmountQuery
            {
                IsGrocery = false,
                Amount = 300,
                CustomerId = id
            }, CancellationToken.None);

            var matchedResult = result.Match<InvoiceDto>(x => x, x => null);

            matchedResult.Amount.Should().Be(210);
        }

        [Fact]
        public async Task Handle_OldCustomer_ReturnDiscount()
        {
            var id = Guid.Parse("FE98F549-E790-4E9F-AA16-18C2292A2EE9");

            var handler = new GetTotalInvoiceAmountQueryHandler(_inventorypository.Object, _customerRepository.Object,
                _discountRepository.Object, _unitOfWork.Object);

            var result = await handler.Handle(new GetTotalInvoiceAmountQuery
            {
                IsGrocery = false,
                Amount = 300,
                CustomerId = id
            }, CancellationToken.None);

            var matchedResult = result.Match<InvoiceDto>(x => x, x => null);

            matchedResult.Amount.Should().Be(285);
        }

        [Fact]
        public async Task Handle_Over100DollarBill_ReturnDiscount()
        {
            var id = Guid.Parse("BF3F3002-7E53-441E-8B76-F6280BE284AA");

            var handler = new GetTotalInvoiceAmountQueryHandler(_inventorypository.Object, _customerRepository.Object,
                _discountRepository.Object, _unitOfWork.Object);

            var result = await handler.Handle(new GetTotalInvoiceAmountQuery
            {
                IsGrocery = false,
                Amount = 990,
                CustomerId = id
            }, CancellationToken.None);

            var matchedResult = result.Match<InvoiceDto>(x => x, x => null);

            matchedResult.Amount.Should().Be(544.5M);
        }

        [Fact]
        public async Task Handle_NormalCustomerWithLessThan100DollarBill_ReturnNoDiscount()
        {
            var id = Guid.Parse("BF3F3002-7E53-441E-8B76-F6280BE284AA");

            var handler = new GetTotalInvoiceAmountQueryHandler(_inventorypository.Object, _customerRepository.Object,
                _discountRepository.Object, _unitOfWork.Object);

            var result = await handler.Handle(new GetTotalInvoiceAmountQuery
            {
                IsGrocery = false,
                Amount = 90,
                CustomerId = id
            }, CancellationToken.None);

            var matchedResult = result.Match<InvoiceDto>(x => x, x => null);

            matchedResult.Amount.Should().Be(90);
        }

        [Fact]
        public async Task Handle_GroceryBill_ReturnNoDiscount()
        {
            var id = Guid.Parse("BF3F3002-7E53-441E-8B76-F6280BE284AA");

            var handler = new GetTotalInvoiceAmountQueryHandler(_inventorypository.Object, _customerRepository.Object,
                _discountRepository.Object, _unitOfWork.Object);

            var result = await handler.Handle(new GetTotalInvoiceAmountQuery
            {
                IsGrocery = true,
                Amount = 990,
                CustomerId = id
            }, CancellationToken.None);

            var matchedResult = result.Match<InvoiceDto>(x => x, x => null);

            matchedResult.Amount.Should().Be(990);
        }
    }
}
