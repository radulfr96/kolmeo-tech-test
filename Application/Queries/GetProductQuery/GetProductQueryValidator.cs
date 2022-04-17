using Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GetProductQuery
{
    public class GetProductQueryValidator : AbstractValidator<GetProductQuery>
    {
        public GetProductQueryValidator()
        {
            RuleFor(c => c.Id).GreaterThan(0).WithErrorCode(ErrorCodeEnum.ProductIdInvalidValue.ToString());
        }
    }
}
