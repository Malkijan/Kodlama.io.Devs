using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Kodlama.io.Devs.Application.Features.OperationClaims.Constants;
using Kodlama.io.Devs.Application.Features.OperationClaims.Dtos;
using Kodlama.io.Devs.Application.Features.OperationClaims.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.OperationClaims.Commands.UpdateOperationClaim
{
    public class UpdateOperationClaimCommand:IRequest<UpdatedOperationClaimDto>,ISecuredRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] Roles { get; } =
        {
            OperationClaimRoles.OperationClaimAdmin,
            OperationClaimRoles.OperationClaimUpdate
        };

        public class UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommand, UpdatedOperationClaimDto>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly OperationClaimBusinessRules _operationClaimBusinessRules;
            private readonly IMapper _mapper;

            public UpdateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, 
                OperationClaimBusinessRules operationClaimBusinessRules, IMapper mapper)
            {
                _operationClaimRepository = operationClaimRepository;
                _operationClaimBusinessRules = operationClaimBusinessRules;
                _mapper = mapper;
            }

            public async Task<UpdatedOperationClaimDto> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _operationClaimBusinessRules.OperationClaimIdShouldBeExist(request.Id);
                await _operationClaimBusinessRules.NameCannotBeDuplicatedWhenRequested(request.Name);

                var operationClaim = await _operationClaimRepository.GetAsync(x => x.Id == request.Id);
                var mappedOperationClaim = _mapper.Map(request,operationClaim);
                var updatedOperationClaim = await _operationClaimRepository.UpdateAsync(mappedOperationClaim);
                var mappedUpdatedOperationClaim = _mapper.Map<UpdatedOperationClaimDto>(updatedOperationClaim);
                return mappedUpdatedOperationClaim;
            }
        }
    }
}
