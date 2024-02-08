using Microsoft.AspNetCore.Http.HttpResults;
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
            var storyDto = new StoryDTO(1, "title", "description", "departament");
            mockService.Setup(s => s.GetAll()).Returns(new List<StoryDTO> { storyDto });
            var controller = new StoriesController(mockService.Object);

            var result = controller.GetAll() as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var stories = result.Value as IEnumerable<StoryViewModel>;
            Assert.NotNull(stories);
            Assert.Single(stories);
            Assert.Equal(storyDto.Id, stories.First().Id);
            Assert.Equal(storyDto.Title, stories.First().Title);
            Assert.Equal(storyDto.Description, stories.First().Description);
            Assert.Equal(storyDto.Departament, stories.First().Departament);
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
        public void GetById_ValidId_Ok()
        {
            var mockService = new Mock<IStoryService>();
            int id = 1;
            var storyDto = new StoryDTO(id, "title", "description", "departament");
            mockService.Setup(s => s.GetById(id)).ReturnsAsync(storyDto);
            var controller = new StoriesController(mockService.Object);

            var result = controller.GetById(id).Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var storyViewModel = result.Value as StoryViewModel;

            Assert.NotNull(storyViewModel);
            Assert.Equal(storyDto.Id, storyViewModel.Id);
            Assert.Equal(storyDto.Title, storyViewModel.Title);
            Assert.Equal(storyDto.Description, storyViewModel.Description);
            Assert.Equal(storyDto.Description, storyViewModel.Description);
        }

        [Fact]
        public void GetById_InvalidId_NotFound()
        {
            var mockService = new Mock<IStoryService>();
            var id = 1;
            mockService.Setup(s => s.GetById(id)).Throws(new InvalidOperationException($"Id: {id} not found"));

            var controller = new StoriesController(mockService.Object);

            var result = controller.GetById(id).Result as NotFoundObjectResult;

            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal($"Id: {id} not found", result.Value);
        }

        [Fact]
        public void Add_ValidStory_Created()
        {
            var mockService = new Mock<IStoryService>();
            var storyRequest = new StoryRequest("title", "description", "departament");
            var storyDto = new StoryDTO(1, storyRequest.Title, storyRequest.Description, storyRequest.Departament);
            mockService.Setup(s => s.Add(storyRequest.Title, storyRequest.Description, storyRequest.Departament)).ReturnsAsync(storyDto);
            var controller = new StoriesController(mockService.Object);

            var result = controller.Add(storyRequest).Result as CreatedResult;

            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);

            var storyViewModel = result.Value as StoryViewModel;
            Assert.NotNull(storyViewModel);
            Assert.Equal(storyRequest.Title, storyViewModel.Title);
            Assert.Equal(storyRequest.Description, storyViewModel.Description);
            Assert.Equal(storyRequest.Departament, storyViewModel.Departament);
        }

        [Fact]
        public void Add_InvalidStory_BadRequest()
        {
            var mockService = new Mock<IStoryService>();
            var storyRequest = new StoryRequest("title", "", "departament");
            mockService.Setup(s => s.Add(storyRequest.Title, storyRequest.Description, storyRequest.Departament)).Throws(new ArgumentException("Invalid Story parameters"));

            var controller = new StoriesController(mockService.Object);

            var result = controller.Add(storyRequest).Result as BadRequestObjectResult;
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Invalid Story parameters", result.Value);
        }

        [Fact]
        public void Update_ValidRequest_Ok()
        {
            var mockService = new Mock<IStoryService>();
            int id = 1;
            var storyRequest = new StoryRequest("title", "description", "departament");
            var storyDto = new StoryDTO(id, storyRequest.Title, storyRequest.Description, storyRequest.Departament);
            mockService.Setup(s => s.Update(storyDto));
            var controller = new StoriesController(mockService.Object);

            var result = controller.Update(id, storyRequest).Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var storyViewModel = result.Value as StoryViewModel;
            Assert.NotNull(storyViewModel);
            Assert.Equal(id, storyViewModel.Id);
            Assert.Equal(storyRequest.Title, storyViewModel.Title);
            Assert.Equal(storyRequest.Description, storyViewModel.Description);
            Assert.Equal(storyRequest.Departament, storyViewModel.Departament);
        }

        [Fact]
        public void Update_InvalidStory_BadRequest()
        {
            var mockService = new Mock<IStoryService>();
            int id = 1;
            var storyRequest = new StoryRequest("title", "description", "departament");
            var storyDto = new StoryDTO(id, storyRequest.Title, storyRequest.Description, storyRequest.Departament);

            mockService.Setup(s => s.Update(storyDto))
                .Throws(new ArgumentException("Invalid Story parameters"));

            var controller = new StoriesController(mockService.Object);

            var result = controller.Update(id, storyRequest).Result as BadRequestObjectResult;
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Invalid Story parameters", result.Value);

        }

        [Fact]
        public void Update_InvalidId_NotFound()
        {
            var mockService = new Mock<IStoryService>();
            int id = 2;
            var storyRequest = new StoryRequest("title", "description", "departament");
            var storyDto = new StoryDTO(id, storyRequest.Title, storyRequest.Description, storyRequest.Departament);
            mockService.Setup(s => s.Update(storyDto)).Throws(new InvalidOperationException($"Id: {storyDto.Id} not found"));

            var controller = new StoriesController(mockService.Object);

            var result = controller.Update(id, storyRequest).Result as NotFoundObjectResult;

            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal($"Id: {storyDto.Id} not found", result.Value);
        }

        [Fact]
        public void Delete_ValidId_Ok()
        {
            var mockService = new Mock<IStoryService>();
            int id = 1;
            mockService.Setup(s => s.Delete(id)).ReturnsAsync(true);
            var controller = new StoriesController(mockService.Object);

            var result = controller.Delete(id).Result as OkResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void Delete_InvalidId_Ok()
        {
            var mockService = new Mock<IStoryService>();
            int id = 1;
            mockService.Setup(s => s.Delete(id)).ReturnsAsync(false);
            var controller = new StoriesController(mockService.Object);

            var result = controller.Delete(id).Result as NotFoundResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
