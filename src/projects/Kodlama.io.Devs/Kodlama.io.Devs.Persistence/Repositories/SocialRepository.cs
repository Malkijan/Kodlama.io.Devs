﻿using Core.Persistence.Repositories;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using Kodlama.io.Devs.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Persistence.Repositories
{
    public class SocialRepository : EfRepositoryBase<Social, BaseDbContext>, ISocialRepository
    {
        public SocialRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
