using AutoMapper;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Features.Socials.Commands.CreateSocial;
using Kodlama.io.Devs.Application.Features.Socials.Commands.DeleteSocial;
using Kodlama.io.Devs.Application.Features.Socials.Commands.UpdateSocial;
using Kodlama.io.Devs.Application.Features.Socials.Dtos;
using Kodlama.io.Devs.Application.Features.Socials.Models;
using Kodlama.io.Devs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Socials.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Social, CreateSocialCommand>().ReverseMap();
            CreateMap<Social, CreatedSocialDto>().
                ForMember(c => c.UserFullName, opt => opt.MapFrom(pl => $"{pl.User.FirstName} {pl.User.LastName}")).
                ReverseMap();

            CreateMap<Social, UpdateSocialCommand>().ReverseMap();
            CreateMap<Social, UpdatedSocialDto>().
                ForMember(c => c.UserFullName, opt => opt.MapFrom(pl => $"{pl.User.FirstName} {pl.User.LastName}"))
                .ReverseMap();

            CreateMap<Social, DeleteSocialCommand>().ReverseMap();
            CreateMap<Social, DeletedSocialDto>().
                ForMember(c => c.UserFullName, opt => opt.MapFrom(pl => $"{pl.User.FirstName} {pl.User.LastName}"))
                .ReverseMap();

            CreateMap<SocialListModel, IPaginate<Social>>().ReverseMap();
            CreateMap<Social, SocialListDto>().
                ForMember(c => c.UserFullName, opt => opt.MapFrom(pl => $"{pl.User.FirstName} {pl.User.LastName}"))
                .ReverseMap();
        }
    }
}
