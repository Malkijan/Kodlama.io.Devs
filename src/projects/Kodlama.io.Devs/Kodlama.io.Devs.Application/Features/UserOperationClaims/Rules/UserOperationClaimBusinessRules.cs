using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Kodlama.io.Devs.Application.Features.UserOperationClaims.Constants;
using Kodlama.io.Devs.Application.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.UserOperationClaims.Rules
{
    public class UserOperationClaimBusinessRules
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOperationClaimRepository _operationClaimRepository;

        public UserOperationClaimBusinessRules(IUserOperationClaimRepository userOperationClaimRepository, 
            IUserRepository userRepository, IOperationClaimRepository operationClaimRepository)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _userRepository = userRepository;
            _operationClaimRepository = operationClaimRepository;
        }

        public async Task UserIdAndOperationClaimIdCannotBeDuplicatedWhenRequested(int userId, int operationClaimId)
        {
            var operationClaim = await _userOperationClaimRepository.GetAsync(x=>x.UserId == userId && x.OperationClaimId == operationClaimId);
            if (operationClaim != null) throw new BusinessException(UserOperationClaimMessages.UserIdAndOperationClaimIdCanNotBeDuplicated);
        }

        public async Task UserOperationClaimIdShouldBeExist(int id)
        {
            var result = await _userOperationClaimRepository.GetListAsync(x => x.Id == id);
            if (!result.Items.Any()) throw new BusinessException(UserOperationClaimMessages.IdShouldBeExist);
        }

        public void OperationClaimShouldExistWhenRequested(UserOperationClaim userOperationClaim)
        {
            if (userOperationClaim is null) throw new BusinessException(UserOperationClaimMessages.DoesNotHaveAnyRecords);
        }

        public async Task CheckIfUserExists(int userId)
        {
            var user = await _userRepository.GetAsync(x => x.Id == userId);
            if (user is null) throw new BusinessException(UserOperationClaimMessages.UserDoesNotExists);
        }

        public async Task CheckIfOperationClaimExists(int operationClaimId)
        {
            var user = await _operationClaimRepository.GetAsync(x => x.Id == operationClaimId);
            if (user is null) throw new BusinessException(UserOperationClaimMessages.OperationClaimDoesNotExists);
        }
    }
}
