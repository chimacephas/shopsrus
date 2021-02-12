using AutoMapper;
using FluentAssertions;
using Moq;
using ShopsRUs.Application.Common.Repository.Interface;
using ShopsRUs.Application.Features.Discounts.Queries;
using ShopsRUs.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using ShopsRUs.Application.Common.Utils;
using ShopsRUs.Application.Features.Discounts.Models;
using ShopsRUs.Application.Common.Models;

namespace ShopsRUs.Tests.Discounts.Queries
{
    public class DiscountQueryTests
    {
        private readonly Mock<IDiscountRepository> _discountRepository;
        private readonly IMapper _mapper;
        public DiscountQueryTests()
        {
            _discountRepository = DiscountRepositoryMock.GetDiscountRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }


        [Fact]
        public async Task GetAllDiscounts_ReturnsAllDiscounts()
        {
            var handler = new GetAllDiscountQueryHandler(_discountRepository.Object, _mapper);

            var result = await handler.Handle(new GetAllDiscountQuery(), CancellationToken.None);

            result.Should().HaveCount(4);
        }

        [Fact]
        public async Task GetDiscountByType_ValidType_ReturnsDiscount()
        {
            var type = Constants.AffliateDiscount;

            var handler = new GetDiscountbyTypeQueryHandler(_discountRepository.Object, _mapper);

            var result = await handler.Handle(new GetDiscountbyTypeQuery(type), CancellationToken.None);

            var matchedResult = result.Match<DiscountDto>(x => x, x => null);

            matchedResult.Type.Should().Be(type);
        }


        [Fact]
        public async Task GetDiscountByType_InValidType_ReturnsNotFound()
        {
            var type = "GeneralDiscount";

            var handler = new GetDiscountbyTypeQueryHandler(_discountRepository.Object, _mapper);

            var result = await handler.Handle(new GetDiscountbyTypeQuery(type), CancellationToken.None);

            var matchedResult = result.Match<NotFound>(x => null, x => x);

            matchedResult.Description.Should().Be("Discount does not exist");
        }
    }
}
