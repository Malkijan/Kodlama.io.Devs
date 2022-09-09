using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Features.Techs.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Techs.Models
{
    public class TechListModel : BasePageableModel
    {
       public IList<TechListDto> Items { get; set; }
    }
}
