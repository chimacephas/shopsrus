using AutoMapper;
using MediatR;
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
    public class GetAllCustomerQuery : IRequest<List<CustomerDto>>
    {
    }

    public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, List<CustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetAllCustomerQueryHandler(ICustomerRepository customerRepository,IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<List<CustomerDto>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<CustomerDto>>(await _customerRepository.GetAllAsync());
        }
    }
}
