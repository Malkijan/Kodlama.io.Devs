using AutoMapper;
using Kodlama.io.Devs.Application.Features.Techs.Dtos;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
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

            public CreateTechCommandHandler(ITechRepository techRepository, IMapper mapper)
            {
                _techRepository = techRepository;
                _mapper = mapper;
            }

            public async Task<CreatedTechDto> Handle(CreateTechCommand request, CancellationToken cancellationToken)
            {
                Tech mappedTech = _mapper.Map<Tech>(request);
                Tech createdTech = await _techRepository.AddAsync(mappedTech);
                CreatedTechDto createdTechDto = _mapper.Map<CreatedTechDto>(createdTech);

                return createdTechDto;

            }
        }
    }
}
