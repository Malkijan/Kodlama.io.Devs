using FluentValidation;
using Kodlama.io.Devs.Application.Features.OperationClaims.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.OperationClaims.Queries.GetByIdOperationClaim
{
    public class GetByIdOperationClaimQueryValidator:AbstractValidator<GetByIdOperationClaimQuery>
    {
        public GetByIdOperationClaimQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage(OperationClaimMessages.IdIsRequired);
            RuleFor(x => x.Id).GreaterThan(0).WithMessage(OperationClaimMessages.GreaterThanZero);
        }
    }
}
