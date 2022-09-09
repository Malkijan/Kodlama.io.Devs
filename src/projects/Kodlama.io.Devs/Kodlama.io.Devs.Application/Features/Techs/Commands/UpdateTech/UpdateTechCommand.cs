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

namespace Kodlama.io.Devs.Application.Features.Techs.Commands.UpdateTech
{
    public class UpdateTechCommand : IRequest<UpdatedTechDto>
    {
        public int Id { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }

        public class UpdateTechCommandHandler : IRequestHandler<UpdateTechCommand, UpdatedTechDto>
        {
            private readonly ITechRepository _techRepository;
            private readonly IMapper _mapper;

            public UpdateTechCommandHandler(ITechRepository techRepository, IMapper mapper)
            {
                _techRepository = techRepository;
                _mapper = mapper;
            }

            public async Task<UpdatedTechDto> Handle(UpdateTechCommand request, CancellationToken cancellationToken)
            {
                Tech? mappedTech = _mapper.Map<Tech>(request);
                Tech updatedTech = await _techRepository.UpdateAsync(mappedTech);
                UpdatedTechDto updatedTechDto = _mapper.Map<UpdatedTechDto>(updatedTech);

                return updatedTechDto;
            }
        }
    }
}
