using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Socials.Commands.CreateSocial
{
    public class CreateSocialCommandValidator:AbstractValidator<CreateSocialCommand>
    {
        public CreateSocialCommandValidator()
        {
            RuleFor(s => s.SocialUrl).NotEmpty();
            RuleFor(s => s.UserId).NotNull();
        }
    }
}
