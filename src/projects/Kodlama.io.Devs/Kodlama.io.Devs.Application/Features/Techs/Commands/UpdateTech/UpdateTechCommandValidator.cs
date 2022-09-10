using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Techs.Commands.UpdateTech
{
    public class UpdateTechCommandValidator:AbstractValidator<UpdateTechCommand>
    {
        public UpdateTechCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
