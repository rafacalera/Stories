using Microsoft.EntityFrameworkCore;
using Stories.API.Data;
using Stories.API.Data.Models;
using Stories.API.Services.Models;

namespace Stories.API.Services
{
    public class StoryService : IStoryService
    {
        private readonly StoriesContext _context;

        private bool IsStoryValid(string title, string description, string departament)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description) || string.IsNullOrWhiteSpace(departament))
            {
                return false;
            }

            if (title.Length > 80 || description.Length > 250 || departament.Length > 50)
            {
                return false;
            } 
            
            return true;
        }

        public StoryService(StoriesContext context)
        {
            _context = context;
        }

        public async Task<StoryDTO> Add(string title, string description, string departament)
        {

            if (!IsStoryValid(title, description, departament))
                throw new ArgumentException("Invalid Story parameters");

            var story = new Story(title, description, departament);



            await _context.Story.AddAsync(story);
            await _context.SaveChangesAsync();
            return new StoryDTO(story.Id, story.Title, story.Description, story.Departament);
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StoryDTO> GetAll()
        {
            return _context.Story.Select(s => new StoryDTO(s.Id, s.Title, s.Description, s.Departament)).AsEnumerable();
        }

        public async Task<StoryDTO> GetById(int id)
        {
            var story = await _context.Story.FirstOrDefaultAsync(f => f.Id == id);

            if (story == default)
                throw new InvalidOperationException($"Id: {id} not found");

            return new StoryDTO(story.Id, story.Title, story.Description, story.Departament);
        }

        public async Task Update(StoryDTO storyDto)
        {
            if (!IsStoryValid(storyDto.Title, storyDto.Description, storyDto.Departament)) throw new ArgumentException("Invalid Story parameters");

            var story = await _context.Story.FirstOrDefaultAsync(f => f.Id == storyDto.Id);

            if (story == default) throw new InvalidOperationException($"Id: {storyDto.Id} not found");

            story.Title = storyDto.Title;
            story.Description = storyDto.Description;
            story.Departament = storyDto.Departament;
            await _context.SaveChangesAsync();
        }
    }
}
