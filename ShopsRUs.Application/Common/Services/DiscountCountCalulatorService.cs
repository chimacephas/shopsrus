using ShopsRUs.Application.Common.Utils;
using ShopsRUs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Application.Common.Services
{
    public class DiscountCountCalulatorService
    {

        public static decimal Calculate(decimal Amount,Discount discount)
        {
            if(discount.Type.ToLower() == Constants.PercentPer100DollarDiscount.ToLower())
            {
                double percentageTpApply = discount.Percentage * (int)(Amount / 100);

                return Amount - (Amount * ((decimal)percentageTpApply / 100));
            }
            else
            {
                return Amount - (Amount * ((decimal)discount.Percentage / 100));
            }
        }
    }
}
