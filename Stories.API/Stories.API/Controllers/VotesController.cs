using Microsoft.AspNetCore.Mvc;
using Stories.API.Application.Models.Requests;
using Stories.API.Services.Interfaces;

namespace Stories.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : ControllerBase
    {
        private readonly IVoteService _service;
        public VotesController(IVoteService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Add(VoteRequest voteRequest)
        {
            bool result = await _service.Add(voteRequest.UpVote, voteRequest.StoryId, voteRequest.UserId);

            if (!result)
                return BadRequest();

            return Created();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _service.Delete(id);

            if (!result)
                return NotFound();

            return Ok();
        }
    }
}
