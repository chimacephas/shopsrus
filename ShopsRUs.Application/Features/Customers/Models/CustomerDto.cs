using ShopsRUs.Application.ProfileMapping;
using ShopsRUs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Application.Features.Customers.Models
{
    public class CustomerDto : IMapFrom<Customer>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsAnAffliate { get; set; }
        public bool IsAnEmployee { get; set; }
    }
}
