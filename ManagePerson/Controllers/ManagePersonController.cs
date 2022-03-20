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
        private readonly IMediator mediator;

        public ManagePersonController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("GetByEmail")]
        public async Task<IActionResult> Get(string email)
        {
            var query = new GetPersonByEmailQuery { Email = email };
            var result = await mediator.Send(query);
            if (result.Count > 0)
                return Ok(result);
            return NotFound();
        }

        [HttpPut("UpdateByEmail")]
        public async Task<IActionResult> Update(EditPersonByEmailCommand editCommand)
        {
            var result = await mediator.Send(editCommand);
            if (result.Count > 0)
                return Ok(result);
            return NotFound();
        }


    }
}
