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
        public async Task<IActionResult> Add(VoteRequest voteRequest)
        {
            bool result = await _service.Add(voteRequest.UpVote, voteRequest.StoryId, voteRequest.UserId);

            if (!result)
                return BadRequest();

            return Ok(voteRequest);
        }
    }
}
