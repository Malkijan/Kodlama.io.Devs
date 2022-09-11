using AutoMapper;
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

namespace Kodlama.io.Devs.Application.Features.Socials.Commands.UpdateSocial
{
    public class UpdateSocialCommand : IRequest<UpdatedSocialDto>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string SocialUrl { get; set; }

        public class UpdateSocialCommandHandler : IRequestHandler<UpdateSocialCommand, UpdatedSocialDto>
        {
            private readonly ISocialRepository _socialRepository;
            private readonly IMapper _mapper;
            private readonly SocialBusinessRules _socialBusinessRules;

            public UpdateSocialCommandHandler(ISocialRepository socialRepository, IMapper mapper, SocialBusinessRules socialBusinessRules)
            {
                _socialRepository = socialRepository;
                _mapper = mapper;
                _socialBusinessRules = socialBusinessRules;
            }

            public async Task<UpdatedSocialDto> Handle(UpdateSocialCommand request, CancellationToken cancellationToken)
            {
                Social mappedSocial = _mapper.Map<Social>(request);

                await _socialBusinessRules.SocialGitHubLinkCanNotBeSame(mappedSocial.SocialUrl);

                Social updateSocial = await _socialRepository.UpdateAsync(mappedSocial);
                Social updatedSocialResult = await _socialRepository.GetAsync(s => s.Id == updateSocial.Id);
                UpdatedSocialDto updatedSocialDto = _mapper.Map<UpdatedSocialDto>(updatedSocialResult);

                return updatedSocialDto;
            }
        }
    }   
}
