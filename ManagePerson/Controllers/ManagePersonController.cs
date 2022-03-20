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

        [HttpGet("Email")]
        public async Task<IActionResult> Get(string email)
        {
            var query = new GetPersonByEmailQuery{ Email = email };
            return Ok(await mediator.Send(query));
        }

        [HttpPost()]
        public async Task<IActionResult> Update(EditPersonByEmailCommand editCommand)
        {
            
            return Ok(await mediator.Send(editCommand));
        }


    }
}
