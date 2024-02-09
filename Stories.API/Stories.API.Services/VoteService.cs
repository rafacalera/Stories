using Microsoft.EntityFrameworkCore;
using Stories.API.Data;
using Stories.API.Data.Models;
using Stories.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories.API.Services
{
    public class VoteService : IVoteService
    {
        private readonly StoriesContext _context;

        public VoteService(StoriesContext context)
        {
            _context = context;
        }

        private async Task<bool> ValidRequest (bool upVote, int storyId, int userId)
        {
            var findedVote = await _context.Vote.FirstOrDefaultAsync(f => f.UserId == userId && f.StoryId == storyId);
            var findedUser = await _context.User.FirstOrDefaultAsync(f => f.Id == userId);
            var findedStory = await _context.Story.FirstOrDefaultAsync(f => f.Id == storyId);

            if (findedVote != default || findedUser == default || findedStory == default)
                return false;

            return true;
        }

        public async Task<bool> Add(bool upVote, int storyId, int userId)
        {
            if (!(await ValidRequest(upVote, storyId, userId)))
                return false;
            
            var vote = new Vote(upVote, storyId, userId);

            await _context.Vote.AddAsync(vote);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var vote = await _context.Vote.FirstOrDefaultAsync(f => f.Id == id);

            if (vote == default)
                return false;

            _context.Vote.Remove(vote);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
