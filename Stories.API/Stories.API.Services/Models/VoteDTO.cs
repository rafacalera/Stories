using Stories.API.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories.API.Services.Models
{
    public class VoteDTO
    {
        public VoteDTO(int id, bool upVote, int storyId, int userId)
        {
            Id = id;
            UpVote = upVote;
            StoryId = storyId;
            UserId = userId;
        }

        public int Id { get; private set; }
        public bool UpVote { get; set; }
        public int StoryId { get; set; }
        public int UserId { get; set; }
    }
}
