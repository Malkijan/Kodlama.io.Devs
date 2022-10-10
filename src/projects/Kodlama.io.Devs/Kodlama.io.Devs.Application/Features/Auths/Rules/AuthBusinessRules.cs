using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Core.Security.Hashing;
using Kodlama.io.Devs.Application.Features.Auths.Constants;
using Kodlama.io.Devs.Application.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Auths.Rules
{
    public class AuthBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public AuthBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task EmailCannotBeDuplicatedWhenRegistered(string email)
        {
            User user = await _userRepository.GetAsync(u=>u.Email==email);
            if (user != null) throw new BusinessException(AuthMessages.EmailCanNotBeDuplicatedWhenRegistered);
        }

        public async Task UserShouldBeExistWhenLogin(string email)
        {
            User user = await _userRepository.GetAsync(u => u.Email == email);
            if (user == null) throw new BusinessException(AuthMessages.UserIsNotFound);
        }

        public void CheckIfPasswordIsCorrect(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            if (!HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt))
            throw new BusinessException(AuthMessages.CheckIfPasswordIsCorrect);
        }
    }
}
