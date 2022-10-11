using FluentValidation;
using Kodlama.io.Devs.Application.Features.OperationClaims.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.OperationClaims.Commands.CreateOperationClaim
{
    public class CreateOperationClaimCommandValidator:AbstractValidator<CreateOperationClaimCommand>
    {
        public CreateOperationClaimCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage(OperationClaimMessages.NameIsRequired);
        }
    }
}
