using FluentValidation;
using Kodlama.io.Devs.Application.Features.Auths.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Auths.Commands.Register
{
    public class RegisterCommandValidator:AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(r => r.UserForRegisterDto.Email).NotEmpty().NotNull().WithMessage(AuthMessages.UserEmailIsRequired);
            RuleFor(r => r.UserForRegisterDto.Email).EmailAddress();
            RuleFor(r => r.UserForRegisterDto.Password).NotEmpty().NotNull().WithMessage(AuthMessages.UserPasswordIsRequired);
            RuleFor(r => r.UserForRegisterDto.Password).MinimumLength(8).WithMessage(AuthMessages.PasswordMinimumLength);
            RuleFor(r => r.UserForRegisterDto.FirstName).NotEmpty().NotNull().WithMessage(AuthMessages.UserFirstNameIsRequired);
            RuleFor(r => r.UserForRegisterDto.LastName).NotEmpty().NotNull().WithMessage(AuthMessages.UserLastNameIsRequired);
        }
    }
}
