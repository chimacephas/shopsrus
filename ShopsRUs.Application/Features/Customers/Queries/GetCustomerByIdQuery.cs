﻿using AutoMapper;
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

    public class GetCustomerByIdQuery : IRequest<OneOf<CustomerDto, NotFound>>
    {
        public readonly Guid _id;

        public GetCustomerByIdQuery(Guid Id)
        {
            _id = Id;
        }
    }

    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, OneOf<CustomerDto, NotFound>>
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<OneOf<CustomerDto, NotFound>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.SingleOrDefaultAsync(x => x.Id == request._id);

            if (customer == null)
                return new NotFound("Customer does not exist");

            return _mapper.Map<CustomerDto>(customer);
        }

    }
}
