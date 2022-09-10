using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
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
    public class GetListTechByDynamicQuery:IRequest<TechListModel>
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }
    }
    public class GetListTechByDynamicQueryHandler : IRequestHandler<GetListTechByDynamicQuery, TechListModel>
    {
        private readonly IMapper _mapper;
        private readonly ITechRepository _techRepository;

        public GetListTechByDynamicQueryHandler(IMapper mapper, ITechRepository techRepository)
        {
            _mapper = mapper;
            _techRepository = techRepository;
        }

        public async Task<TechListModel> Handle(GetListTechByDynamicQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Tech> techs = await _techRepository.GetListByDynamicAsync(dynamic: request.Dynamic,
                                     include: t => t.Include(pl => pl.ProgrammingLanguage),
                                     index: request.PageRequest.Page,
                                     size: request.PageRequest.PageSize
                                     );
            TechListModel model = _mapper.Map<TechListModel>(techs);
            
            return model;
        }
    }
}
