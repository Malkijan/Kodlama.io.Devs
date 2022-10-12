using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Dtos;
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

namespace Kodlama.io.Devs.Application.Features.Techs.Commands.DeleteTech
{
    public class DeleteTechCommand : IRequest<DeletedTechDto>,ISecuredRequest
    {
        public int Id { get; set; }
        public string[] Roles { get; } =
{
        TechRoles.TechAdmin,
        TechRoles.TechDelete
        };

        public class DeleteTechCommandHandler : IRequestHandler<DeleteTechCommand, DeletedTechDto>
        {
            private readonly ITechRepository _techRepository;
            private readonly IMapper _mapper;
            private readonly TechBusinessRules _techBusinessRules;

            public DeleteTechCommandHandler(ITechRepository techRepository, IMapper mapper, TechBusinessRules techBusinessRules)
            {
                _techRepository = techRepository;
                _mapper = mapper;
                _techBusinessRules = techBusinessRules;
            }

            public async Task<DeletedTechDto> Handle(DeleteTechCommand request, CancellationToken cancellationToken)
            {
                await _techBusinessRules.TechShouldExistsWhenRequested(request.Id);

                Tech? tech = await _techRepository.GetAsync(x => x.Id == request.Id);
                Tech deletedTech = await _techRepository.DeleteAsync(tech);

                DeletedTechDto deletedTechDto = _mapper.Map<DeletedTechDto>(deletedTech);

                return deletedTechDto;
            }
        }
    }
}
