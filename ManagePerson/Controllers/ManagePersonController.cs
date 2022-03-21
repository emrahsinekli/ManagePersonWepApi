using ManagePerson.CQRS.Commands;
using ManagePerson.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ManagePerson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagePersonController : ControllerBase
    {
        public IMediator Mediator { get; set; }

        public ManagePersonController(IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpGet("GetByEmail")]
        public async Task<IActionResult> Get(string email)
        {
            var query = new GetPersonByEmailQuery { Email = email };
            var result = await Mediator.Send(query);
            if (result.Count > 0)
                return Ok(result);
            return NotFound();
        }

        [HttpPut("UpdateByEmail")]
        public async Task<IActionResult> Update(EditPersonByEmailCommand editCommand)
        {
            var result = await Mediator.Send(editCommand);
            if (result.Count > 0)
                return Ok(result);
            return NotFound();
        }



    }
}
