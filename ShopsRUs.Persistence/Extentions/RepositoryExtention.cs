using Microsoft.Extensions.DependencyInjection;
using ShopsRUs.Application.Common.Repository;
using ShopsRUs.Application.Common.Repository.Interface;
using ShopsRUs.Persistence.Repository;
using ShopsRUs.Persistence.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Persistence.Extentions
{
    public static class RepositoryExtention
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IDiscountRepository, DiscountRepository>();
            services.AddTransient<IInvoiceRepository, InvoiceRepository>();

        }
    }
}
