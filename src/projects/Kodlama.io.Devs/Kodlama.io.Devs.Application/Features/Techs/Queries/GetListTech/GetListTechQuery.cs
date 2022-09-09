using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Features.Techs.Dtos;
using Kodlama.io.Devs.Application.Features.Techs.Models;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Techs.Queries.GetListTech
{
    public class GetListTechQuery : IRequest<TechListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListTechQueryHandler : IRequestHandler<GetListTechQuery, TechListModel>
        {
            private readonly IMapper _mapper;
            private readonly ITechRepository _techRepository;

            public GetListTechQueryHandler(IMapper mapper, ITechRepository techRepository)
            {
                _mapper = mapper;
                _techRepository = techRepository;
            }

            public async Task<TechListModel> Handle(GetListTechQuery request, CancellationToken cancellationToken)
            {
              IPaginate<Tech> techs = await _techRepository.GetListAsync(include: 
                                            t=>t.Include(pl=>pl.ProgrammingLanguage),
                                            index :request.PageRequest.Page,
                                            size: request.PageRequest.PageSize);

                TechListModel mappedTechs = _mapper.Map<TechListModel>(techs);
                return mappedTechs;

            }
        }
    }
}
