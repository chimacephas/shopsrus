using MediatR;
using OneOf;
using ShopsRUs.Application.Common.Models;
using ShopsRUs.Application.Common.Repository;
using ShopsRUs.Application.Common.Repository.Interface;
using ShopsRUs.Application.Common.Services;
using ShopsRUs.Application.Common.Utils;
using ShopsRUs.Application.Features.Invoices.Models;
using ShopsRUs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopsRUs.Application.Features.Invoices.Queries
{
    public class GetTotalInvoiceAmountQuery : IRequest<OneOf<InvoiceDto, NotFound>>
    {
        public decimal Amount { get; set; }
        public Guid CustomerId { get; set; }
        public bool IsGrocery { get; set; }
    }

    public class GetTotalInvoiceAmountQueryHandler : IRequestHandler<GetTotalInvoiceAmountQuery, OneOf<InvoiceDto, NotFound>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetTotalInvoiceAmountQueryHandler(IInvoiceRepository invoiceRepository,
            ICustomerRepository customerRepository,IDiscountRepository discountRepository,
            IUnitOfWork unitOfWork)
        {
            _invoiceRepository = invoiceRepository;
            _customerRepository = customerRepository;
            _discountRepository = discountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OneOf<InvoiceDto, NotFound>> Handle(GetTotalInvoiceAmountQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.SingleOrDefaultAsync(x => x.Id == request.CustomerId);

            if (customer == null)
                return new NotFound("Customer does not exist");


            var discount = await _discountRepository.GetAllAsync(x=>x.Percentage > 0);

            decimal discountedPrice = request.Amount;

            if (customer.IsAnAffliate && !request.IsGrocery)
            {
                discountedPrice = DiscountCountCalulatorService
                    .Calculate(request.Amount, discount.FirstOrDefault(x=> x.Type.ToLower() == Constants.AffliateDiscount));
            }
            else if (customer.IsAnEmployee && !request.IsGrocery)
            {
                discountedPrice = DiscountCountCalulatorService
                .Calculate(request.Amount, discount.FirstOrDefault(x => x.Type.ToLower() == Constants.EmployeeDiscount));
            }
            else if (customer.CreatedAt <= DateTime.Now.AddYears(-2) && !request.IsGrocery)
            {
                discountedPrice = DiscountCountCalulatorService
                .Calculate(request.Amount, discount.FirstOrDefault(x => x.Type.ToLower() == Constants.OldCustomerDiscount));
            }
            else if (!request.IsGrocery)
            {
                discountedPrice = DiscountCountCalulatorService
                .Calculate(request.Amount, discount.FirstOrDefault(x => x.Type.ToLower() == Constants.PercentPer100DollarDiscount.ToLower()));
            }

            _invoiceRepository.Add(new Invoice
            {
                Id = Guid.NewGuid(),
                Amount = discountedPrice,
                CustomerId = request.CustomerId,
                CreatedAt = DateTime.Now
            });

            await _unitOfWork.SaveChangesAsync();

            return new InvoiceDto { Amount = discountedPrice, Customer = customer.Name };
        }
    }
}
