using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Kodlama.io.Devs.Application.Features.OperationClaims.Constants;
using Kodlama.io.Devs.Application.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.OperationClaims.Rules
{
    public class OperationClaimBusinessRules
    {
        private readonly IOperationClaimRepository _operationClaimRepository;

        public OperationClaimBusinessRules(IOperationClaimRepository operationClaimRepository)
        {
            _operationClaimRepository = operationClaimRepository;
        }

        public async Task NameCannotBeDuplicatedWhenRequested (string name)
        {
            OperationClaim operationClaim = await _operationClaimRepository.GetAsync(x => x.Name == name);
            if (operationClaim != null) throw new BusinessException(OperationClaimMessages.NameIsRequired);
        }

        public async Task OperationClaimIdShouldBeExist(int id)
        {
            var result = await _operationClaimRepository.GetListAsync(x => x.Id == id);
            if(!result.Items.Any()) throw new BusinessException(OperationClaimMessages.IdShouldBeExist);
        }
    }
}
