using AutoMapper;
using ShopsRUs.Application.Features.Discounts.Commands;
using ShopsRUs.Application.ProfileMapping;
using ShopsRUs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Application.Features.Discounts.Models
{
    public class DiscountCreationDto : IMapFrom<Discount>
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public double Percentage { get; set; }
        public decimal Amount { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DiscountCreationDto, CreateDiscountCommand>();
            profile.CreateMap<CreateDiscountCommand, Discount>();
        }
    }
}
