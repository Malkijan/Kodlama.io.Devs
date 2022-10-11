using FluentValidation;
using Kodlama.io.Devs.Application.Features.UserOperationClaims.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim
{
    public class CreateUserOperationClaimCommandValidator : AbstractValidator<CreateUserOperationClaimCommand>
    {
        public CreateUserOperationClaimCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().NotNull().WithMessage(UserOperationClaimMessages.UserIdIsRequired);
            RuleFor(x => x.OperationClaimId).NotEmpty().NotNull().WithMessage(UserOperationClaimMessages.OperationClaimIdIsRequired);
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage(UserOperationClaimMessages.UserIdGreaterThanZero);
            RuleFor(x => x.OperationClaimId).GreaterThan(0).WithMessage(UserOperationClaimMessages.OperationClaimIdGreaterThanZero);

        }
    }
}
