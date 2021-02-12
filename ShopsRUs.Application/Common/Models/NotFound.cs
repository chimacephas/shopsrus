using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Application.Common.Models
{
    public class NotFound
    {
        public string Description { get; set; }
        public NotFound(string description)
        {
            Description = description;
        }
    }

    public class Error
    {
        public string Description { get; set; }
        public Error(string description)
        {
            Description = description;
        }
    }
}
