using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum ErrorCodeEnum
    {
        ProductNotFound = 1,
        ProductIdInvalidValue = 2,
        NameNotProvided = 3,
        DescriptionNotProvided = 4,
        PriceInvalidValue = 5,
    }
}
