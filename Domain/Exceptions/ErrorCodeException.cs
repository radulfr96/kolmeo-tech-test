﻿using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ErrorCodeException : Exception
    {
        public ErrorCodeEnum ErrorCode { get; protected set; }

        public ErrorCodeException(ErrorCodeEnum errorCode, string message = null, Exception inner = null)
        : base(message, inner)
        {
            ErrorCode = errorCode;
        }
    }
}
