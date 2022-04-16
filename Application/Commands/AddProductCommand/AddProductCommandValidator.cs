using Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.AddProductCommand
{
    public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithErrorCode(ErrorCodeEnum.NameNotProvided.ToString());
            RuleFor(c => c.Description).NotEmpty().WithErrorCode(ErrorCodeEnum.DescriptionNotProvided.ToString());
            RuleFor(c => c.Price).GreaterThan(0.00m).WithErrorCode(ErrorCodeEnum.PriceInvalidValue.ToString());
        }
    }
}
