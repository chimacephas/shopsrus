using AutoMapper;
using MediatR;
using OneOf;
using ShopsRUs.Application.Common.Models;
using ShopsRUs.Application.Common.Repository.Interface;
using ShopsRUs.Application.Features.Discounts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopsRUs.Application.Features.Discounts.Queries
{
    public class GetDiscountbyTypeQuery : IRequest<OneOf<DiscountDto, NotFound>>
    {
        public readonly string _type;

        public GetDiscountbyTypeQuery(string type)
        {
            _type = type;
        }
    }

    public class GetDiscountbyTypeQueryHandler : IRequestHandler<GetDiscountbyTypeQuery, OneOf<DiscountDto, NotFound>>
    {

        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public GetDiscountbyTypeQueryHandler(IDiscountRepository discountRepository, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        public async Task<OneOf<DiscountDto, NotFound>> Handle(GetDiscountbyTypeQuery request, CancellationToken cancellationToken)
        {
            var discount = _mapper.Map<DiscountDto>(await _discountRepository.SingleOrDefaultAsync(x => x.Type.ToLower() == request._type.ToLower()));

            if (discount == null)
                return new NotFound("Discount does not exist");

            return _mapper.Map<DiscountDto>(discount);
        }

    }
}
