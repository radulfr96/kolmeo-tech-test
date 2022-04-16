using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class BadRequestException : ErrorCodeException
    {
        public BadRequestException(ErrorCodeEnum errorCode, string message = null, Exception inner = null) : base(errorCode, message, inner)
        {
        }
    }
}
