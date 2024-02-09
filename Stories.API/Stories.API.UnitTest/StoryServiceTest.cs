using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using Stories.API.Data;
using Stories.API.Data.Models;
using Stories.API.Services;
using Stories.API.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories.API.UnitTest
{
    public class StoryServiceTest
    {
        private readonly StoriesContext _context;

        public StoryServiceTest()
        {
            DbContextOptions<StoriesContext> options = new DbContextOptionsBuilder<StoriesContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new StoriesContext(options);
        }


        [Fact]
        public async Task Add_ValidParameters_Int()
        {
            var service = new StoryService(_context);
            var title = "title";
            var description = "description";
            var departament = "departament";

            int id = await service.Add(title, description, departament);

            IEnumerable<Story> stories = _context.Story.AsEnumerable();
            Assert.NotNull(stories.First());
            Assert.Equal(stories.First().Id, id);
            Assert.Equal(title, stories.First().Title);
            Assert.Equal(description, stories.First().Description);
            Assert.Equal(departament, stories.First().Departament);
        }

        [Theory]
        [InlineData(null, "description", "")]
        [InlineData("", "", "")]
        [InlineData("title", "", "")]
        [InlineData("title", "description", "")]
        public async Task Add_InvalidParameters_ArgumentException(string title, string description, string departament)
        {
            var service = new StoryService(_context);
            Exception exception = null;

            try
            {
                await service.Add(title, description, departament);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            
            Assert.NotNull(exception);
            Assert.IsType<ArgumentException>(exception);
            Assert.Equal("Invalid Story parameters", exception.Message);
        }

        [Fact]
        public void GetAll_IEnumerableOfStoryDTO()
        {
            var service = new StoryService(_context);

            _context.Story.Add(new Story("title1", "description1", "departament1"));
            _context.Story.Add(new Story("title2", "description2", "departament2"));
            _context.Story.Add(new Story("title3", "description3", "departament3"));
            _context.SaveChanges();

            var result = service.GetAll();

            
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<StoryDTO>>(result);
            Assert.Equal(3, result.Count());


        }

        [Fact]
        public async Task GetById_ValidId_StoryDTO()
        {
            var service = new StoryService(_context);
            int id = 1;
            _context.Story.Add(new Story("title1", "description1", "departament1"));
            _context.SaveChanges();


            var result = await service.GetById(id);

            Assert.NotNull(result);
            Assert.IsType<StoryDTO>(result);
            Assert.Equal(id, result.Id);
            Assert.Equal("title1", result.Title);
            Assert.Equal("description1", result.Description);
            Assert.Equal("departament1", result.Departament);
        }

        [Fact]
        public async Task GetById_InvalidId_InvalidOperationException()
        {
            var service = new StoryService(_context);
            int id = 1;
            Exception exception = null;

            try
            {
                var storyDto = await service.GetById(id);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.NotNull(exception);
            Assert.IsType<InvalidOperationException>(exception);
            Assert.Equal($"Id: {id} not found", exception.Message);

        }

        [Fact]
        public async Task Update_ValidParameters()
        {
            var service = new StoryService(_context);
            _context.Story.Add(new Story("title", "description", "departament"));
            _context.SaveChanges();
            var storyDto = new StoryDTO(1, "newTitle", "newDescription", "newDepartment");

            await service.Update(storyDto);

            var story = _context.Story.Find(storyDto.Id);
            Assert.NotNull(story);
            Assert.Equal(storyDto.Title, story.Title);
            Assert.Equal(storyDto.Description, story.Description);
            Assert.Equal(storyDto.Departament, story.Departament);

        }

        [Theory]
        [InlineData(null, "newDescription", "")]
        [InlineData("", "", "")]
        [InlineData("newTitle", "", "")]
        [InlineData("newTitle", "newDescription", "")]
        public async Task Update_InvalidParameters_ArgumentException(string title, string description, string departament)
        {
            var service = new StoryService(_context);

            _context.Story.Add(new Story("title", "description", "departament"));
            _context.SaveChanges();

            var storyDto = new StoryDTO(1, title, description, departament);

            Exception exception = null;

            try
            {
                await service.Update(storyDto);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.NotNull(exception);
            Assert.IsType<ArgumentException>(exception);
            Assert.Equal("Invalid Story parameters", exception.Message);
        }

        [Fact]
        public async Task Update_InvalidId_InvalidOperationException()
        {
            var service = new StoryService(_context);
            Exception exception = null;
            var storyDto = new StoryDTO(1, "newTitle", "newDescription", "newDepartment");

            try
            {
                await service.Update(storyDto);
            }
            catch (Exception ex)
            {
                exception = ex;           
            }

            Assert.NotNull(exception);
            Assert.IsType<InvalidOperationException>(exception);
            Assert.Equal($"Id: {storyDto.Id} not found", exception.Message);
        }

        [Fact]
        public async Task Delete_ValidId_True()
        {
            var service = new StoryService(_context);
            int id = 1;
            _context.Story.Add(new Story("title", "description", "departament"));
            _context.SaveChanges();

            bool result = await service.Delete(id);

            Assert.True(result);
            Assert.Equal(0, _context.Story.Count());
        }

        public async Task Delete_InvalidId_False()
        {
            var service = new StoryService(_context);

            bool result = await service.Delete(1);

            Assert.False(result);
        }
    }
}
