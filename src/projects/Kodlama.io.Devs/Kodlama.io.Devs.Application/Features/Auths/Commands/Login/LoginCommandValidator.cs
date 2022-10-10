using FluentValidation;
using Kodlama.io.Devs.Application.Features.Auths.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Auths.Commands.Login
{
    public class LoginCommandValidator:AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.userForLoginDto.Email).NotEmpty().NotNull().WithMessage(AuthMessages.UserEmailIsRequired);
            RuleFor(x => x.userForLoginDto.Email).EmailAddress();

            RuleFor(x=>x.userForLoginDto.Password).NotEmpty().NotNull().WithMessage(AuthMessages.UserPasswordIsRequired);
        }
    }
}
