using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories.API.Data.Models
{
    public class Vote
    {
        public Vote(bool upVote, string user)
        {
            UpVote = upVote;
            User = user;
        }

        public int Id { get; private set; }
        public bool UpVote { get; set; }
        public string User { get; set; }
        public int StoryId { get; set; }
        public Story Story { get; set; } = null!;
    }
}
