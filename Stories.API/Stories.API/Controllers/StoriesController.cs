using Microsoft.AspNetCore.Mvc;
using Stories.API.Application.Models.ViewModels;
using Stories.API.Application.Models.Requests;
using Stories.API.Services.Models;
using Stories.API.Services.Interfaces;
using Stories.API.Application.Commands.Requests;
using MediatR;
using Stories.API.Application.Queries.Requests;
using Stories.API.Application.Queries.Responses;

namespace Stories.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoriesController : ControllerBase
    {
        private readonly IStoryService _service;

        public StoriesController(IStoryService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FindStoryResponse>), 200)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> GetAll([FromServices] IMediator mediator)
        {
            var response = await mediator.Send(new FindAllStoriesRequest());

            if (response.Count == 0)
                return NoContent();

            return Ok(response.AsEnumerable());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FindStoryResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById([FromServices] IMediator mediator, int id)
        {
            var response = await mediator.Send(new FindStoryByIdRequest(id));

            if (response == null)
                return NotFound();

            return Ok(response);

        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateStoryRequest), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Add([FromServices] IMediator mediator, [FromBody] CreateStoryRequest command)
        {
            int response = await mediator.Send(command);

            if (response == 0)
                return BadRequest();

            return Ok(response);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<IActionResult> Update([FromServices] IMediator mediator, int id, [FromBody] StoryRequest storyRequest)
        {
            var response = await mediator
                .Send(new UpdateStoryRequest(id, storyRequest.Title, storyRequest.Description, storyRequest.Departament));

            if (response == false)
                return BadRequest();

            if (response == null)
                return NotFound();


            return Ok();

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete([FromServices] IMediator mediator, int id)
        {
            if (!await mediator.Send(new DeleteStoryRequest(id)))
                return NotFound();

            return Ok();
        }

        [HttpPost("{id}/Vote")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Vote([FromServices] IMediator mediator, int id, [FromBody] VoteRequest voteRequest)
        {
            var response = await mediator.Send(new CreateStoryVoteRequest(voteRequest.UpVote, id, voteRequest.UserId));

            if (response == false)
                return BadRequest();

            if (response == null)
                return NotFound();

            return Ok();
        }
    }
}
