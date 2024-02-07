using Stories.API.Data;
using Stories.API.Data.Models;
using Stories.API.Services.Models;

namespace Stories.API.Services
{
    public class StoryService : IStoryService
    {
        private readonly StoriesContext _context;

        private bool IsStoryValid(string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description))
            {
                return false;
            }

            if (title.Length > 80 || description.Length > 250)
            {
                return false;
            } 
            
            return true;
        }

        public StoryService(StoriesContext context)
        {
            _context = context;
        }

        public async Task<StoryDTO> Add(string title, string description)
        {

            if (!IsStoryValid(title, description))
                throw new ArgumentException("Invalid Story parameters");

            var story = new Story(title, description);
            var poll = new Poll();

            story.Poll = poll;


            await _context.Story.AddAsync(story);
            await _context.SaveChangesAsync();
            return new StoryDTO(story.Id, story.Title, story.Description, story.Poll.Id);
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StoryDTO> GetAll()
        {
            return _context.Story.Select(s => new StoryDTO(s.Id, s.Title, s.Description, s.Poll.Id)).AsEnumerable();
        }

        public Task<StoryDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(int id, string title, string description)
        {
            throw new NotImplementedException();
        }
    }
}
