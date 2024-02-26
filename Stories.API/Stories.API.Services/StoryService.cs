using Microsoft.EntityFrameworkCore;
using Stories.API.Data;
using Stories.API.Data.Models;
using Stories.API.Services.Interfaces;
using Stories.API.Services.Models;

namespace Stories.API.Services
{
    public class StoryService : IStoryService
    {
        private readonly StoriesContext _context;

        public StoryService(StoriesContext context)
        {
            _context = context;
        }

        private async Task ValidVote(int storyId, int userId)
        {
            if (await _context.Story.FirstOrDefaultAsync(f => f.Id == storyId) == default)
                throw new InvalidOperationException("Story doesn't exists");

            if (await _context.User.FirstOrDefaultAsync(f => f.Id == userId) == default)
                throw new ArgumentException("User doesn't exists");

            if (await _context.Vote.FirstOrDefaultAsync(f => f.UserId == userId && f.StoryId == storyId) != default)
                throw new ArgumentException("User already vote");
        }


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

        public async Task<int> Add(string title, string description, string departament)
        {

            if (!IsStoryValid(title, description, departament))
                throw new ArgumentException("Invalid Story parameters");

            var story = new Story(title, description, departament);



            await _context.Story.AddAsync(story);
            await _context.SaveChangesAsync();
            return story.Id;
        }

        public IEnumerable<StoryDTO> GetAll()
        {
            return _context.Story
                .Include(s => s.Votes)
                .ThenInclude(x => x.User)
                .Select(s => new StoryDTO(
                        s.Id,
                        s.Title,
                        s.Description,
                        s.Departament)
                { Votes = s.Votes.Select(v => new VoteDTO(v.Id, v.UpVote, v.StoryId, v.UserId)).ToList() }).AsEnumerable();
        }

        public async Task<StoryDTO> GetById(int id)
        {
            var story = await _context.Story
                                        .Include(s => s.Votes)
                                        .ThenInclude(v => v.User)
                                        .FirstOrDefaultAsync(f => f.Id == id);

            if (story == default)
                throw new InvalidOperationException($"Id: {id} not found");

            return new StoryDTO(story.Id, story.Title, story.Description, story.Departament)
            {
                Votes = story.Votes.Select(v => new VoteDTO(v.Id, v.UpVote, v.StoryId, v.UserId)).ToList()
            };
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


        public async Task<bool> Delete(int id)
        {
            var story = await _context.Story.FirstOrDefaultAsync(f => f.Id == id);

            if (story == default) return false;

            _context.Story.Remove(story);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<int> Vote(bool upVote, int storyId, int userId)
        {
            await ValidVote(storyId, userId);

            var vote = new Vote(upVote, storyId, userId);

            await _context.Vote.AddAsync(vote);
            await _context.SaveChangesAsync();
            return vote.Id;
        }
    }
}
