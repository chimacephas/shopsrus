using AutoMapper;
using ShopsRUs.Application.Features.Customers.Commands;
using ShopsRUs.Application.Features.Customers.Models;
using ShopsRUs.Application.Features.Discounts.Commands;
using ShopsRUs.Application.Features.Discounts.Models;
using ShopsRUs.Domain.Entities;

namespace ShopsRUs.Tests
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerCreationDto, CreateCustomerCommand>();
            CreateMap<CreateCustomerCommand, Customer>();

            CreateMap<Discount, DiscountDto>();
            CreateMap<DiscountCreationDto, CreateDiscountCommand>();
            CreateMap<CreateDiscountCommand, Discount>();
        }
    }
}
