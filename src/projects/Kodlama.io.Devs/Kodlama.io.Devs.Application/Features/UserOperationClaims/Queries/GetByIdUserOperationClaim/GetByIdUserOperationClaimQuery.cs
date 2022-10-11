﻿using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Kodlama.io.Devs.Application.Features.UserOperationClaims.Constants;
using Kodlama.io.Devs.Application.Features.UserOperationClaims.Dtos;
using Kodlama.io.Devs.Application.Features.UserOperationClaims.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.UserOperationClaims.Queries.GetByIdUserOperationClaim
{
    public class GetByIdUserOperationClaimQuery : IRequest<UserOperationClaimGetByIdDto>,ISecuredRequest
    {
        public int Id { get; set; }

        public string[] Roles { get; } =
        {
        UserOperationClaimRoles.UserOperationClaimAdmin,
        UserOperationClaimRoles.UserOperationClaimRead
        };

        public class GetByIdUserOperationClaimQueryHandler : IRequestHandler<GetByIdUserOperationClaimQuery, UserOperationClaimGetByIdDto>
        {
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMapper _mapper;
            private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

            public GetByIdUserOperationClaimQueryHandler(IUserOperationClaimRepository userOperationClaimRepository, 
                IMapper mapper, UserOperationClaimBusinessRules userOperationClaimBusinessRules)
            {
                _userOperationClaimRepository = userOperationClaimRepository;
                _mapper = mapper;
                _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
            }

            public async Task<UserOperationClaimGetByIdDto> Handle(GetByIdUserOperationClaimQuery request, CancellationToken cancellationToken)
            {
                await _userOperationClaimBusinessRules.UserOperationClaimIdShouldBeExist(request.Id);

                var userOperationClaim = await _userOperationClaimRepository.Query()
                    .Include(x=>x.OperationClaim)
                    .Include(x=>x.User)
                    .FirstOrDefaultAsync(x=>x.Id == request.Id, cancellationToken: cancellationToken);

                var mappedUserOperationClaim = _mapper.Map<UserOperationClaimGetByIdDto>(userOperationClaim);
                return mappedUserOperationClaim;
            }
        }
    }
}