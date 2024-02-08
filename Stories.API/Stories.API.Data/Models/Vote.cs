using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories.API.Data.Models
{
    public class Vote
    {
        public Vote(bool upVote)
        {
            UpVote = upVote;
        }

        public int Id { get; private set; }
        public bool UpVote { get; set; }
        public int StoryId { get; set; }
        public Story Story { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
