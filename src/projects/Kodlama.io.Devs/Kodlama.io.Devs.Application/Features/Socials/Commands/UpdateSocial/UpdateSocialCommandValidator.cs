using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Socials.Commands.UpdateSocial
{
    public class UpdateSocialCommandValidator: AbstractValidator<UpdateSocialCommand>
    {
        public UpdateSocialCommandValidator()
        {
            RuleFor(u => u.Id).NotEmpty();
            RuleFor(u=>u.SocialUrl).NotEmpty();
        }
    }
}
