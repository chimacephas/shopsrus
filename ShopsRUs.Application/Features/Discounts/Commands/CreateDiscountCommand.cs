using AutoMapper;
using MediatR;
using OneOf;
using ShopsRUs.Application.Common.Models;
using ShopsRUs.Application.Common.Repository;
using ShopsRUs.Application.Common.Repository.Interface;
using ShopsRUs.Application.Features.Discounts.Models;
using ShopsRUs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopsRUs.Application.Features.Discounts.Commands
{
    public class CreateDiscountCommand : IRequest<OneOf<DiscountDto, Error>>
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public double Percentage { get; set; }
        public decimal Amount { get; set; }
    }

    public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, OneOf<DiscountDto, Error>>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateDiscountCommandHandler(IDiscountRepository discountRepository, IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _discountRepository = discountRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OneOf<DiscountDto, Error>> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            if (_discountRepository.DoesExist(x => x.Type.ToLower() == request.Type.ToLower()))
                return new Error("Discount type already exist");

            var discount = _mapper.Map<Discount>(request);
            discount.Id = Guid.NewGuid();
            discount.CreatedAt = DateTime.Now;

            _discountRepository.Add(discount);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<DiscountDto>(discount);
        }
    }
}
