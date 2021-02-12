using AutoMapper;
using MediatR;
using OneOf;
using ShopsRUs.Application.Common.Models;
using ShopsRUs.Application.Common.Repository.Interface;
using ShopsRUs.Application.Features.Customers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopsRUs.Application.Features.Customers.Queries
{

    public class GetCustomerByNameQuery : IRequest<OneOf<CustomerDto, NotFound>>
    {
        public readonly string _name;

        public GetCustomerByNameQuery(string name)
        {
            _name = name;
        }
    }

    public class GetCustomerByNameQueryHandler : IRequestHandler<GetCustomerByNameQuery, OneOf<CustomerDto, NotFound>>
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerByNameQueryHandler(ICustomerRepository customerRepository,IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<OneOf<CustomerDto, NotFound>> Handle(GetCustomerByNameQuery request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<CustomerDto>(await _customerRepository.SingleOrDefaultAsync(x => x.Name.ToLower() == request._name.ToLower()));

            if (customer == null)
                return new NotFound("Customer does not exist");

            return _mapper.Map<CustomerDto>(customer);
        }

    }
}
