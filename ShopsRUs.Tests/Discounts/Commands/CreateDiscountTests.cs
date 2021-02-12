using AutoMapper;
using FluentAssertions;
using Moq;
using ShopsRUs.Application.Common.Repository;
using ShopsRUs.Application.Common.Repository.Interface;
using ShopsRUs.Application.Features.Discounts.Commands;
using ShopsRUs.Application.Features.Discounts.Models;
using ShopsRUs.Domain.Entities;
using ShopsRUs.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using ShopsRUs.Application.Common.Utils;
using ShopsRUs.Application.Common.Models;

namespace ShopsRUs.Tests.Discounts.Commands
{
    public class CreateDiscountTests
    {
        private readonly Mock<IDiscountRepository> _discountRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly IMapper _mapper;
        public CreateDiscountTests()
        {
            _discountRepository = DiscountRepositoryMock.GetDiscountRepository();
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }


        [Fact]
        public async Task Handle_ExistingDiscountType_ReturnError()
        {
            var handler = new CreateDiscountCommandHandler(_discountRepository.Object, _unitOfWork.Object, _mapper);

            var result = await handler.Handle(new CreateDiscountCommand 
            { Name = "New Customer Discount",
              Type = Constants.AffliateDiscount,
              Percentage = 10
            }, CancellationToken.None);

            var matchedResult = result.Match<Error>(x => null, x => x);

            _discountRepository.Verify(x => x.Add(It.IsAny<Discount>()), Times.Never);
            _unitOfWork.Verify(x => x.SaveChangesAsync(), Times.Never);
            matchedResult.Description.Should().Be("Discount type already exist");
        }



        [Fact]
        public async Task Handle_NonExistingDiscountType_AddsDiscountToRepo()
        {
            var handler = new CreateDiscountCommandHandler(_discountRepository.Object, _unitOfWork.Object, _mapper);

            var result = await handler.Handle(new CreateDiscountCommand
            {
                Name = "New Customer Discount",
                Type = "NewCustomerDiscount",
                Percentage = 10
            }, CancellationToken.None);

            var matchedResult = result.Match<DiscountDto>(x => x, x => null);

            _discountRepository.Verify(x => x.Add(It.IsAny<Discount>()), Times.Once);
            _unitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
            matchedResult.Should().BeOfType<DiscountDto>();
        }
    }
}
