using ShopsRUs.Application.ProfileMapping;
using ShopsRUs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Application.Features.Discounts.Models
{
    public class DiscountDto : IMapFrom<Discount>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Percentage { get; set; }
        public decimal Amount { get; set; }
    }
}
