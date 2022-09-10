using AutoMapper;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Features.Techs.Commands.CreateTech;
using Kodlama.io.Devs.Application.Features.Techs.Commands.DeleteTech;
using Kodlama.io.Devs.Application.Features.Techs.Commands.UpdateTech;
using Kodlama.io.Devs.Application.Features.Techs.Dtos;
using Kodlama.io.Devs.Application.Features.Techs.Models;
using Kodlama.io.Devs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Techs.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Tech, CreatedTechDto>().ForMember(c=>c.ProgrammingLanguageName, opt=>opt.MapFrom(pl=>pl.ProgrammingLanguage.Name)).ReverseMap();
            CreateMap<Tech, CreateTechCommand>().ReverseMap();

            CreateMap<Tech, UpdatedTechDto>().ForMember(c => c.ProgrammingLanguageName, opt => opt.MapFrom(pl => pl.ProgrammingLanguage.Name)).ReverseMap();
            CreateMap<Tech, UpdateTechCommand>().ReverseMap();

            CreateMap<Tech, DeletedTechDto>().ReverseMap();
            CreateMap<Tech, DeleteTechCommand>().ReverseMap();

            CreateMap<Tech, TechListDto>().ForMember(t=>t.ProgrammingLanguageName, opt=>opt.MapFrom(pl=>pl.ProgrammingLanguage.Id)).ReverseMap();

            CreateMap<IPaginate<Tech>, TechListModel>().ReverseMap();
        }
    }
}
