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

namespace Kodlama.io.Devs.Application.Features.Socials.Commands.DeleteSocial
{
    public class DeleteSocialCommand:IRequest<DeletedSocialDto>
    {
        public int Id { get; set; }

        public class DeleteSocialCommandHandler : IRequestHandler<DeleteSocialCommand, DeletedSocialDto>
        {
            private readonly ISocialRepository _socialRepository;
            private readonly IMapper _mapper;
            private readonly SocialBusinessRules _socialBusinessRules;

            public DeleteSocialCommandHandler(ISocialRepository socialRepository, IMapper mapper, SocialBusinessRules socialBusinessRules)
            {
                _socialRepository = socialRepository;
                _mapper = mapper;
                _socialBusinessRules = socialBusinessRules;
            }

            public async Task<DeletedSocialDto> Handle(DeleteSocialCommand request, CancellationToken cancellationToken)
            {
                
                Social mappedSocial = _mapper.Map<Social>(request);

                await _socialBusinessRules.SocialGitHubLinkChecked(mappedSocial.UserId);

                Social deleteSocial = await _socialRepository.GetAsync(s=>s.Id == request.Id);
                Social deletedSocial = await _socialRepository.DeleteAsync(mappedSocial);
                DeletedSocialDto deletedSocialDto = _mapper.Map<DeletedSocialDto>(deleteSocial);

                return deletedSocialDto;
            }
        }
    }
}
