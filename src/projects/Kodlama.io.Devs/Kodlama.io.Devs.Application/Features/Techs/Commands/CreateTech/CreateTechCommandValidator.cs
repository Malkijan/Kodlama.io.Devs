using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Techs.Commands.CreateTech
{
    public class CreateTechCommandValidator :AbstractValidator<CreateTechCommand>
    {
        public CreateTechCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
