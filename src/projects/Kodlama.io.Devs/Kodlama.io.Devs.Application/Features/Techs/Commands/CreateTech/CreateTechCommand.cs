using AutoMapper;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Rules;
using Kodlama.io.Devs.Application.Features.Techs.Dtos;
using Kodlama.io.Devs.Application.Features.Techs.Rules;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Techs.Commands.CreateTech
{
    public class CreateTechCommand : IRequest<CreatedTechDto>
    {
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }

        public class CreateTechCommandHandler : IRequestHandler<CreateTechCommand, CreatedTechDto>
        {
            private readonly ITechRepository _techRepository;
            private readonly IMapper _mapper;
            private readonly TechBusinessRules _techBusinessRules;
            private readonly ProgrammingLanguageBusinessRules _programmingLanguageBusinessRules;

            public CreateTechCommandHandler(ITechRepository techRepository, IMapper mapper, 
                TechBusinessRules techBusinessRules, ProgrammingLanguageBusinessRules programmingLanguageBusinessRules)
            {
                _techRepository = techRepository;
                _mapper = mapper;
                _techBusinessRules = techBusinessRules;
                _programmingLanguageBusinessRules = programmingLanguageBusinessRules;
            }

            public async Task<CreatedTechDto> Handle(CreateTechCommand request, CancellationToken cancellationToken)
            {
                await _techBusinessRules.TechNameCanNotBeDuplicatedWhenInserted(request.Name);
                await _programmingLanguageBusinessRules.ProgrammingLanguageShouldExistWhenRequested(request.ProgrammingLanguageId);

                Tech mappedTech = _mapper.Map<Tech>(request);
                Tech createdTech = await _techRepository.AddAsync(mappedTech);
                Tech createdTechResult = await _techRepository.GetAsync(t=>t.Id == createdTech.Id);
                CreatedTechDto createdTechDto = _mapper.Map<CreatedTechDto>(createdTechResult);

                return createdTechDto;

            }
        }
    }
}
