using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Kodlama.io.Devs.Application.Features.Socials.Constants;
using Kodlama.io.Devs.Application.Features.Socials.Dtos;
using Kodlama.io.Devs.Application.Features.Socials.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Socials.Commands.CreateSocial
{
    public class CreateSocialCommand : IRequest<CreatedSocialDto>,ISecuredRequest
    {
        public int UserId { get; set; }
        public string SocialUrl { get; set; }
        public string[] Roles { get; } =
        {
        SocialRoles.SocialAdmin,
        SocialRoles.SocialCreate
        };

        public class CreateSocialCommandHandler : IRequestHandler<CreateSocialCommand, CreatedSocialDto>
        {
            private readonly ISocialRepository _socialRepository;
            private readonly IMapper _mapper;
            private readonly SocialBusinessRules _socialBusinessRules;

            public CreateSocialCommandHandler(ISocialRepository socialRepository, IMapper mapper, SocialBusinessRules socialBusinessRules)
            {
                _socialRepository = socialRepository;
                _mapper = mapper;
                _socialBusinessRules = socialBusinessRules;
            }

            public async Task<CreatedSocialDto> Handle(CreateSocialCommand request, CancellationToken cancellationToken)
            {
                await _socialBusinessRules.SocialGitHubLinkCanNotBeDuplicatedWhenInserted(request.UserId);

                Social mappedSocial = _mapper.Map<Social>(request);
                Social createdSocial = await _socialRepository.AddAsync(mappedSocial);
                Social createdSocialResult = await _socialRepository.GetAsync(s=>s.Id==createdSocial.Id);
                CreatedSocialDto createdSocialDto = _mapper.Map<CreatedSocialDto>(createdSocialResult);

                return createdSocialDto;
            }
        }
    }
}
