using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Rules;
using Kodlama.io.Devs.Application.Features.Techs.Constants;
using Kodlama.io.Devs.Application.Features.Techs.Dtos;
using Kodlama.io.Devs.Application.Features.Techs.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Techs.Commands.UpdateTech
{
    public class UpdateTechCommand : IRequest<UpdatedTechDto>,ISecuredRequest
    {
        public int Id { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }
        public string[] Roles { get; } =
{
        TechRoles.TechAdmin,
        TechRoles.TechUpdate
        };

        public class UpdateTechCommandHandler : IRequestHandler<UpdateTechCommand, UpdatedTechDto>
        {
            private readonly ITechRepository _techRepository;
            private readonly IMapper _mapper;
            private readonly TechBusinessRules _techBusinessRules;
            private readonly ProgrammingLanguageBusinessRules _programmingLanguageBusinessRules;

            public UpdateTechCommandHandler(ITechRepository techRepository, IMapper mapper, TechBusinessRules techBusinessRules, 
                ProgrammingLanguageBusinessRules programmingLanguageBusinessRules)
            {
                _techRepository = techRepository;
                _mapper = mapper;
                _techBusinessRules = techBusinessRules;
                _programmingLanguageBusinessRules = programmingLanguageBusinessRules;
            }

            public async Task<UpdatedTechDto> Handle(UpdateTechCommand request, CancellationToken cancellationToken)
            {
                await _techBusinessRules.TechCannotBeDuplicatedWhenUpdating(request.Id, request.Name);
                await _programmingLanguageBusinessRules.ProgrammingLanguageShouldExistWhenRequested(request.ProgrammingLanguageId);

                Tech? mappedTech = _mapper.Map<Tech>(request);
                Tech updatedTech = await _techRepository.UpdateAsync(mappedTech);
                Tech updatedTechResult = await _techRepository.GetAsync(t=>t.Id == updatedTech.Id);
                UpdatedTechDto updatedTechDto = _mapper.Map<UpdatedTechDto>(updatedTechResult);

                return updatedTechDto;
            }
        }
    }
}
