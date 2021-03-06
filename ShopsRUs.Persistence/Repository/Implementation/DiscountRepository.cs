﻿using ShopsRUs.Application.Common.Repository.Interface;
using ShopsRUs.Domain.Entities;
using ShopsRUs.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Persistence.Repository.Implementation
{
    public class DiscountRepository : Repository<Discount>, IDiscountRepository
    {
        public DiscountRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
