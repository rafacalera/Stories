using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Stories.API.Application.Models.Requests;
using Stories.API.Application.Models.ViewModels;
using Stories.API.Controllers;
using Stories.API.Data.Models;
using Stories.API.Services.Interfaces;
using Stories.API.Services.Models;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Stories.API.UnitTest
{
    //public class StoryControllerTest
    //{
    //    [Fact]
    //    public void GetAll_HasStories_Ok()
    //    {
    //        var stories = new List<StoryDTO>
    //        {
    //        new StoryDTO(1, "title", "description", "departament") { Votes = new List<VoteDTO>() },
    //        };

    //        var serviceMock = new Mock<IStoryService>();
    //        serviceMock.Setup(s => s.GetAll()).Returns(stories);

    //        var controller = new StoriesController(serviceMock.Object);

    //        var result = controller.GetAll();

    //        Assert.NotNull(result);
    //        var okResult = Assert.IsType<OkObjectResult>(result);
    //        var storyViewModels = Assert.IsAssignableFrom<IEnumerable<StoryViewModel>>(okResult.Value);
    //        Assert.NotEmpty(storyViewModels);
    //    }

    //    [Fact]
    //    public void GetAll_NoStories_NoContent()
    //    {
    //        var mockService = new Mock<IStoryService>();
    //        mockService.Setup(s => s.GetAll()).Returns(new List<StoryDTO>());
    //        var controller = new StoriesController(mockService.Object);

    //        var result = controller.GetAll() as NoContentResult;

    //        Assert.NotNull(result);
    //        Assert.Equal(204, result.StatusCode);
    //    }

    //    [Fact]
    //    public async Task GetById_ValidId_Ok()
    //    {
    //        var mockService = new Mock<IStoryService>();
    //        int id = 1;
    //        var storyDto = new StoryDTO(id, "title", "description", "departament") { Votes = new List<VoteDTO>() };
    //        mockService.Setup(s => s.GetById(id)).ReturnsAsync(storyDto);
    //        var controller = new StoriesController(mockService.Object);

    //        var result = await controller.GetById(id) as OkObjectResult;

    //        Assert.NotNull(result);
    //        Assert.Equal(200, result.StatusCode);

    //        var storyViewModel = result.Value as StoryViewModel;

    //        Assert.NotNull(storyViewModel);
    //        Assert.Equal(storyDto.Id, storyViewModel.Id);
    //        Assert.Equal(storyDto.Title, storyViewModel.Title);
    //        Assert.Equal(storyDto.Description, storyViewModel.Description);
    //        Assert.Equal(storyDto.Description, storyViewModel.Description);
    //    }

    //    [Fact]
    //    public void GetById_InvalidId_NotFound()
    //    {
    //        var mockService = new Mock<IStoryService>();
    //        var id = 1;
    //        mockService.Setup(s => s.GetById(id)).Throws(new InvalidOperationException($"Id: {id} not found"));

    //        var controller = new StoriesController(mockService.Object);

    //        var result = controller.GetById(id).Result as NotFoundObjectResult;

    //        Assert.NotNull(result);
    //        Assert.Equal(404, result.StatusCode);
    //        Assert.Equal($"Id: {id} not found", result.Value);
    //    }

    //    [Fact]
    //    public void Add_ValidStory_Created()
    //    {
    //        var mockService = new Mock<IStoryService>();
    //        var id = 1;
    //        var storyRequest = new StoryRequest("title", "description", "departament");
    //        var storyDto = new StoryDTO(1, storyRequest.Title, storyRequest.Description, storyRequest.Departament);
    //        mockService.Setup(s => s.Add(storyRequest.Title, storyRequest.Description, storyRequest.Departament)).ReturnsAsync(id);
    //        var controller = new StoriesController(mockService.Object);

    //        var result = controller.Add(storyRequest).Result as CreatedResult;

    //        Assert.NotNull(result);
    //        Assert.Equal(201, result.StatusCode);
    //        Assert.Equal($"api/Stories/{id}", result.Location);

    //        var storyViewModel = result.Value as StoryViewModel;
    //        Assert.NotNull(storyViewModel);
    //        Assert.Equal(storyRequest.Title, storyViewModel.Title);
    //        Assert.Equal(storyRequest.Description, storyViewModel.Description);
    //        Assert.Equal(storyRequest.Departament, storyViewModel.Departament);
    //    }

    //    [Fact]
    //    public void Add_InvalidStory_BadRequest()
    //    {
    //        var mockService = new Mock<IStoryService>();
    //        var storyRequest = new StoryRequest("title", "", "departament");
    //        mockService.Setup(s => s.Add(storyRequest.Title, storyRequest.Description, storyRequest.Departament)).Throws(new ArgumentException("Invalid Story parameters"));

    //        var controller = new StoriesController(mockService.Object);

    //        var result = controller.Add(storyRequest).Result as BadRequestObjectResult;
    //        Assert.NotNull(result);
    //        Assert.Equal(400, result.StatusCode);
    //        Assert.Equal("Invalid Story parameters", result.Value);
    //    }

    //    [Fact]
    //    public async Task Update_ValidRequest_Ok()
    //    {
    //        int id = 1;
    //        var storyRequest = new StoryRequest("Test Title", "Test Description", "Test Departament");
    //        var serviceMock = new Mock<IStoryService>();
    //        serviceMock.Setup(x => x.Update(It.IsAny<StoryDTO>()))
    //                   .Callback<StoryDTO>(dto =>
    //                   {
    //                       Assert.Equal(id, dto.Id);
    //                       Assert.Equal(storyRequest.Title, dto.Title);
    //                       Assert.Equal(storyRequest.Description, dto.Description);
    //                       Assert.Equal(storyRequest.Departament, dto.Departament);
    //                   });
    //        var controller = new StoriesController(serviceMock.Object);

    //        var result = await controller.Update(id, storyRequest);

    //        Assert.NotNull(result);
    //        var okResult = Assert.IsType<OkObjectResult>(result);
    //        var storyViewModel = Assert.IsType<StoryViewModel>(okResult.Value);
    //        Assert.Equal(id, storyViewModel.Id);
    //        Assert.Equal(storyRequest.Title, storyViewModel.Title);
    //        Assert.Equal(storyRequest.Description, storyViewModel.Description);
    //        Assert.Equal(storyRequest.Departament, storyViewModel.Departament);
    //        serviceMock.Verify(x => x.Update(It.IsAny<StoryDTO>()), Times.Once);
    //    }

    //    [Fact]
    //    public async Task Update_InvalidArguments_BadRequest()
    //    {
    //        int id = 1;
    //        var storyRequest = new StoryRequest("title", "", "");

    //        var serviceMock = new Mock<IStoryService>();
    //        serviceMock.Setup(x => x.Update(It.IsAny<StoryDTO>()))
    //                   .Throws(new ArgumentException("Invalid Story parameters"));

    //        var controller = new StoriesController(serviceMock.Object);

    //        var result = await controller.Update(id, storyRequest);

    //        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
    //        Assert.Equal("Invalid Story parameters", badRequestResult.Value);

    //        serviceMock.Verify(x => x.Update(It.IsAny<StoryDTO>()), Times.Once);

    //    }

    //    [Fact]
    //    public async Task Update_InvalidId_NotFound()
    //    {
    //        int id = 1;
    //        var storyRequest = new StoryRequest("title", "description", "departament");
    //        var serviceMock = new Mock<IStoryService>();
    //        serviceMock.Setup(x => x.Update(It.IsAny<StoryDTO>()))
    //                   .Throws(new InvalidOperationException($"Id: {id} not found"));
    //        var controller = new StoriesController(serviceMock.Object);

    //        var result = await controller.Update(id, storyRequest);

    //        Assert.NotNull(result);
    //        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
    //        Assert.Equal($"Id: {id} not found", notFoundResult.Value);
    //        serviceMock.Verify(x => x.Update(It.IsAny<StoryDTO>()), Times.Once);
    //    }

    //    [Fact]
    //    public void Delete_ValidId_Ok()
    //    {
    //        var mockService = new Mock<IStoryService>();
    //        int id = 1;
    //        mockService.Setup(s => s.Delete(id)).ReturnsAsync(true);
    //        var controller = new StoriesController(mockService.Object);

    //        var result = controller.Delete(id).Result as OkResult;

    //        Assert.NotNull(result);
    //        Assert.Equal(200, result.StatusCode);
    //    }

    //    [Fact]
    //    public void Delete_InvalidId_NotFound()
    //    {
    //        var mockService = new Mock<IStoryService>();
    //        int id = 1;
    //        mockService.Setup(s => s.Delete(id)).ReturnsAsync(false);
    //        var controller = new StoriesController(mockService.Object);

    //        var result = controller.Delete(id).Result as NotFoundResult;

    //        Assert.NotNull(result);
    //        Assert.Equal(404, result.StatusCode);
    //    }

    //    [Fact]
    //    public void Vote_ValidRequest_Ok()
    //    {
    //        var mockService = new Mock<IStoryService>();
    //        int id = 1;
    //        int storyId = 1;
    //        bool upVote = true;
    //        int userId = 1;

    //        mockService.Setup(s => s.Vote(upVote, storyId, userId)).ReturnsAsync(id);

    //        var controller = new StoriesController(mockService.Object);

    //        var result = controller.Vote(storyId, new VoteRequest(upVote, userId)).Result as OkObjectResult;
    //        Assert.NotNull(result);

    //        var voteViewModel = result.Value as VoteViewModel;

    //        Assert.Equal(200, result.StatusCode);
    //        Assert.IsType<VoteViewModel>(voteViewModel);
    //        Assert.Equal(id, voteViewModel.Id);
    //    }

    //    [Theory]
    //    [InlineData("User doesn't exists")]
    //    [InlineData("User already vote")] 
    //    public void Vote_InvalidArguments_BadRequest(string errorMessage)
    //    {
    //        var mockService = new Mock<IStoryService>();

    //        int id = 1;
    //        int storyId = 1;
    //        bool upVote = true;
    //        int userId = 10000;


    //        mockService.Setup(s => s.Vote(upVote, storyId, userId)).Throws(new ArgumentException(errorMessage));

    //        var controller = new StoriesController(mockService.Object);

    //        var result = controller.Vote(storyId, new VoteRequest(upVote, userId)).Result as BadRequestObjectResult;
    //        Assert.NotNull(result);

    //        Assert.Equal(400, result.StatusCode);
    //        Assert.Equal(errorMessage, result.Value);
    //    }

    //    [Fact]
    //    public void Vote_InvalidStoryId_NotFound()
    //    {
    //        var mockService = new Mock<IStoryService>();
    //        int id = 1;
    //        int storyId = 0;
    //        bool upVote = true;
    //        int userId = 1;
    //        string errorMessage = "Story doesn't exists";

    //        mockService.Setup(s => s.Vote(upVote, storyId, userId)).Throws(new InvalidOperationException(errorMessage));

    //        var controller = new StoriesController(mockService.Object);

    //        var result = controller.Vote(storyId, new VoteRequest(upVote, userId)).Result as NotFoundObjectResult;
    //        Assert.NotNull(result);

    //        Assert.Equal(404, result.StatusCode);
    //        Assert.Equal(errorMessage, result.Value);
    //    }
    //}
}
