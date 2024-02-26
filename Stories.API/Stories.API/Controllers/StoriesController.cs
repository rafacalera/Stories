using Microsoft.AspNetCore.Mvc;
using Stories.API.Application.Models.ViewModels;
using Stories.API.Application.Models.Requests;
using Stories.API.Services.Models;
using Stories.API.Services.Interfaces;

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
        [ProducesResponseType(typeof(IEnumerable<StoryViewModel>), 200)]
        [ProducesResponseType(204)]
        public IActionResult GetAll()
        {
            var stories = _service.GetAll();

            if (stories.Count() == 0) return NoContent();

            return Ok(stories.Select(s => new StoryViewModel(s.Id, s.Title, s.Description, s.Departament) 
            { Votes = s.Votes.Select(s => new VoteViewModel(s.Id, s.UpVote, s.UserId)).ToList() }).AsEnumerable());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StoryViewModel), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var storyDto = await _service.GetById(id);
                return Ok(new StoryViewModel(storyDto.Id, storyDto.Title, storyDto.Description, storyDto.Departament) { Votes = storyDto.Votes.Select(s => new VoteViewModel(s.Id, s.UpVote, s.UserId)).ToList() });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            
        }

        [HttpPost]
        [ProducesResponseType(typeof(StoryViewModel), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Add(StoryRequest storyRequest)
        {
            try
            {
                int id = await _service.Add(storyRequest.Title, storyRequest.Description, storyRequest.Departament);
                return Created($"api/Stories/{id}", new StoryViewModel(id, storyRequest.Title, storyRequest.Description, storyRequest.Departament));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        [ProducesResponseType(typeof(StoryViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<IActionResult> Update(int id, StoryRequest storyRequest)
        {
            try
            {
                await _service.Update(new StoryDTO(id, storyRequest.Title, storyRequest.Description, storyRequest.Departament));
                return Ok(new StoryViewModel(id, storyRequest.Title, storyRequest.Description, storyRequest.Departament));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _service.Delete(id)) return NotFound();

            return Ok();
        }

        [HttpPost("{id}/Vote")]
        [ProducesResponseType(typeof(VoteViewModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Vote(int id, VoteRequest voteRequest)
        {
            try
            {
                int voteId = await _service.Vote(voteRequest.UpVote, id, voteRequest.UserId);

                return Ok(new VoteViewModel(voteId, voteRequest.UpVote, voteRequest.UserId));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
