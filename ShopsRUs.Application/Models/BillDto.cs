using AutoMapper;
using ShopsRUs.Application.Features.Invoices.Queries;
using ShopsRUs.Application.ProfileMapping;
using ShopsRUs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Application.Models
{
    public class BillDto : IMapFrom<Invoice>
    {
        public decimal Amount { get; set; }
        public Guid CustomerId { get; set; }
        public bool IsGrocery { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BillDto, GetTotalInvoiceAmountQuery>();
        }
    }
}
