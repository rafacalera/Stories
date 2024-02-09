using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Stories.API.Application.Models.Requests;
using Stories.API.Controllers;
using Stories.API.Data;
using Stories.API.Data.Models;
using Stories.API.Services;
using Stories.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories.API.UnitTest
{
    public class VoteServiceTest 
    {
        private readonly StoriesContext _context;

        public VoteServiceTest()
        {
            DbContextOptions<StoriesContext> options = new DbContextOptionsBuilder<StoriesContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new StoriesContext(options);
        }

        //[Fact]
        //public async Task Add_ValidVote_True()
        //{
        //    var service = new VoteService(_context);
        //    bool upVote = true;
        //    int storyId = 1;
        //    int userId = 1;

        //    bool result = await service.Add(upVote, storyId, userId);

        //    IEnumerable<Vote> votes = _context.Vote.AsEnumerable();
        //    Assert.NotNull(votes.First());
        //    Assert.Equal(upVote, votes.First().UpVote);
        //    Assert.Equal(storyId, votes.First().StoryId);
        //    Assert.Equal(userId, votes.First().UserId);
        //}

      
    }
}
