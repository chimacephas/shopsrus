using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Application.Features.Invoices.Models
{
    public class InvoiceDto
    {
        public decimal Amount { get; set; }
        public string Customer { get; set; }
    }
}
