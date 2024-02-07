using Microsoft.AspNetCore.Mvc;
using Moq;
using Stories.API.Application.Models.Requests;
using Stories.API.Application.Models.ViewModels;
using Stories.API.Controllers;
using Stories.API.Services;
using Stories.API.Services.Models;

namespace Stories.API.UnitTest
{
    public class StoryControllerTest
    {
        [Fact]
        public void GetAll_HasStories_Ok()
        {
            var mockService = new Mock<IStoryService>();
            var storyDto = new StoryDTO(1, "title", "description", 1);
            mockService.Setup(s => s.GetAll()).Returns(new List<StoryDTO> { storyDto });
            var controller = new StoriesController(mockService.Object);

            var result = controller.GetAll() as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var stories = result.Value as IEnumerable<StoryViewModel>;
            Assert.NotNull(stories);
            Assert.Single(stories);
            Assert.Equal(storyDto.Title, stories.First().Title);
            Assert.Equal(storyDto.Description, stories.First().Description);
        }

        [Fact]
        public void GetAll_NoStories_NoContent()
        {
            var mockService = new Mock<IStoryService>();
            mockService.Setup(s => s.GetAll()).Returns(new List<StoryDTO>());
            var controller = new StoriesController(mockService.Object);

            var result = controller.GetAll() as NoContentResult;

            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public void Add_ValidStory_Created()
        {
            var mockService = new Mock<IStoryService>();
            var storyRequest = new StoryRequest("title", "description");
            var storyDto = new StoryDTO(1, storyRequest.Title, storyRequest.Description, 1);
            mockService.Setup(s => s.Add(storyRequest.Title, storyRequest.Description)).ReturnsAsync(storyDto);
            var controller = new StoriesController(mockService.Object);

            var result = controller.Add(storyRequest).Result as CreatedResult;

            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);

            var storyViewModel = result.Value as StoryViewModel;
            Assert.NotNull(storyViewModel);
            Assert.Equal(storyRequest.Title, storyViewModel.Title);
            Assert.Equal(storyRequest.Description, storyViewModel.Description);
        }

        [Fact]
        public void Add_InValidStory_BadRequest()
        {
            var mockService = new Mock<IStoryService>();
            var storyRequest = new StoryRequest("title", "");
            mockService.Setup(s => s.Add(storyRequest.Title, storyRequest.Description)).Throws(new ArgumentException("Invalid Story parameters"));

            var controller = new StoriesController(mockService.Object);

            var result = controller.Add(storyRequest).Result as BadRequestObjectResult;
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Invalid Story parameters", result.Value);
        }
    }
}