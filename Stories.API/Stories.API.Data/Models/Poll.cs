using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories.API.Data.Models
{
    public class Poll
    {
        public int Id { get; private set; }

        public int StoryId { get; private set; }

        public Story Story { get; set; } = null!;

        public ICollection<Vote> Votes { get; } = new List<Vote>();
    }
}
