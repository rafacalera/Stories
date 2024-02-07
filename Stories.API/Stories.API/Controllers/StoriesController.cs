using Microsoft.AspNetCore.Mvc;
using Stories.API.Services;
using Stories.API.Application.Models.ViewModels;
using Stories.API.Application.Models.Requests;

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
        public IActionResult GetAll()
        {
            var stories = _service.GetAll();

            if (stories.Count() == 0) return NoContent();

            return Ok(stories.Select(s => new StoryViewModel(s.Title, s.Description, s.PollId)).AsEnumerable());
        }

        [HttpPost]
        [ProducesResponseType(typeof(StoryViewModel), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Add(StoryRequest storyRequest)
        {
            try
            {
                var cityDto = await _service.Add(storyRequest.Title, storyRequest.Description);
                return Created($"api/Stories/{cityDto.Id}", new StoryViewModel(cityDto.Title, cityDto.Description, cityDto.PollId));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
