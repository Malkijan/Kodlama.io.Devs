using FluentValidation;
using Kodlama.io.Devs.Application.Features.UserOperationClaims.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaim
{
    public class DeleteUserOperationClaimCommandValidator:AbstractValidator<DeleteUserOperationClaimCommand>
    {
        public DeleteUserOperationClaimCommandValidator()
        {
            RuleFor(x=>x.Id).NotEmpty().NotNull().WithMessage(UserOperationClaimMessages.IdIsRequired);
            RuleFor(x => x.Id).GreaterThan(0).WithMessage(UserOperationClaimMessages.IdGreaterThanZero);
        }
    }
}
