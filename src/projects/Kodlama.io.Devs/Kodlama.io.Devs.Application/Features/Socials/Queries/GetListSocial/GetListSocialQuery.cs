using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Features.Socials.Constants;
using Kodlama.io.Devs.Application.Features.Socials.Models;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Socials.Queries.GetListSocial
{
    public class GetListSocialQuery: IRequest<SocialListModel>,ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }
        public string[] Roles { get; } =
{
        SocialRoles.SocialAdmin,
        SocialRoles.SocialRead
        };

        public class GetListSocialQueryHandler : IRequestHandler<GetListSocialQuery, SocialListModel>
        {
            private readonly ISocialRepository _socialRepository;
            private readonly IMapper _mapper;

            public GetListSocialQueryHandler(ISocialRepository socialRepository, IMapper mapper)
            {
                _socialRepository = socialRepository;
                _mapper = mapper;
            }

            public async Task<SocialListModel> Handle(GetListSocialQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Social> socials = await _socialRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

                SocialListModel socialListModel = _mapper.Map<SocialListModel>(socials);

                return socialListModel;
            }
        }
    }
}
