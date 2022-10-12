using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Kodlama.io.Devs.Application.Features.UserOperationClaims.Constants;
using Kodlama.io.Devs.Application.Features.UserOperationClaims.Dtos;
using Kodlama.io.Devs.Application.Features.UserOperationClaims.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaim
{
    public class DeleteUserOperationClaimCommand : IRequest<DeletedUserOperationClaimDto>,ISecuredRequest
    {
        public int Id { get; set; }
        public string[] Roles { get; } =
        {
        UserOperationClaimRoles.UserOperationClaimAdmin,
        UserOperationClaimRoles.UserOperationClaimDelete
        };

        public class DeleteUserOperationClaimCommandHandler : IRequestHandler<DeleteUserOperationClaimCommand, DeletedUserOperationClaimDto>
        {
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;
            private readonly IMapper _mapper;

            public DeleteUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, 
                UserOperationClaimBusinessRules userOperationClaimBusinessRules, IMapper mapper)
            {
                _userOperationClaimRepository = userOperationClaimRepository;
                _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
                _mapper = mapper;
            }

            public async Task<DeletedUserOperationClaimDto> Handle(DeleteUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _userOperationClaimBusinessRules.UserOperationClaimIdShouldBeExist(request.Id);

                var userOperationClaim = await _userOperationClaimRepository.GetAsync(x => x.Id == request.Id);
                var deletedUserOperationClaim = await _userOperationClaimRepository.DeleteAsync(userOperationClaim);
                var mappedDeletedUserOperationClaim = _mapper.Map<DeletedUserOperationClaimDto>(deletedUserOperationClaim);
                return mappedDeletedUserOperationClaim;
            }
        }
    }
}
