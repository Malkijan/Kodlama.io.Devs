using Core.Application.Requests;
using Kodlama.io.Devs.Application.Features.Techs.Commands.CreateTech;
using Kodlama.io.Devs.Application.Features.Techs.Commands.DeleteTech;
using Kodlama.io.Devs.Application.Features.Techs.Commands.UpdateTech;
using Kodlama.io.Devs.Application.Features.Techs.Dtos;
using Kodlama.io.Devs.Application.Features.Techs.Models;
using Kodlama.io.Devs.Application.Features.Techs.Queries.GetListTech;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kodlama.io.Devs.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateTechCommand createTechCommand)
        {
            CreatedTechDto result = await Mediator.Send(createTechCommand);
            return Created("", result);
        }
        
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteTechCommand deleteTechCommand)
        {
            DeletedTechDto result = await Mediator.Send(deleteTechCommand);
            return Ok(result);
        }
        
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] UpdateTechCommand updateTechCommand)
        {
            UpdatedTechDto result = await Mediator.Send(updateTechCommand);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTechQuery getListTechQuery = new GetListTechQuery { PageRequest = pageRequest};
            TechListModel result = await Mediator.Send(getListTechQuery);
            return Ok(result);
        }
    }
}
