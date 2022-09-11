using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Features.Socials.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Socials.Models
{
    public class SocialListModel:BasePageableModel
    {
        public IList<SocialListDto> Items { get; set; }
    }
}
