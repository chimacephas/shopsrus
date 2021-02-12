using AutoMapper;
using ShopsRUs.Application.Features.Customers.Commands;
using ShopsRUs.Application.ProfileMapping;
using ShopsRUs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Application.Features.Customers.Models
{
    public class CustomerCreationDto : IMapFrom<Customer>
    {
        public string Name { get; set; }
        public bool IsAnAffliate { get; set; }
        public bool IsAnEmployee { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerCreationDto, CreateCustomerCommand>();
            profile.CreateMap<CreateCustomerCommand, Customer>();
        }
    }
}
