using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Techs.Rules
{
    public class TechBusinessRules
    {
        private readonly ITechRepository _techRepository;

        public TechBusinessRules(ITechRepository techRepository)
        {
            _techRepository = techRepository;
        }

        public async Task TechNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<Tech> result = await _techRepository.GetListAsync(t => t.Name == name);
            if (result.Items.Any()) throw new BusinessException("Tech name exists");
        }

        public async Task TechShouldExistsWhenRequested(int id)
        {
            Tech? result = await _techRepository.GetAsync(t => t.Id == id);
            if (result == null) throw new BusinessException("Requested tech does not exists.");
        }

        public async Task TechCannotBeDuplicatedWhenUpdating(int id, string name)
        {
            IPaginate<Tech> result = await _techRepository.GetListAsync(t => t.Name == name && t.Id != id);
            if (result.Items.Any()) throw new BusinessException("Tech name already exist.");
        }
    }
}
