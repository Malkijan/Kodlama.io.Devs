using Core.CrossCuttingConcerns.Exceptions;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Socials.Rules
{
    public class SocialBusinessRules
    {
        private readonly ISocialRepository _socialRepository;

        public SocialBusinessRules(ISocialRepository socialRepository)
        {
            _socialRepository = socialRepository;
        }

        public async Task SocialGitHubLinkCanNotBeDuplicatedWhenInserted(int userId)
        {
            Social result = await _socialRepository.GetAsync(s => s.UserId == userId);
            if (result != null) throw new BusinessException("This GitHub link is already exist.");
        }

        public async Task SocialGitHubLinkChecked(int userId)
        {
            Social result = await _socialRepository.GetAsync(s => s.UserId == userId);
            if (result != null) throw new BusinessException("User has not any GitHub link");
        }

        public async Task SocialGitHubLinkCanNotBeSame(string socialUrl)
        {
            Social result = await _socialRepository.GetAsync(s=>s.SocialUrl == socialUrl);
            if (result != null) throw new BusinessException("GitHub link can not be same!");
        }
    }
}
