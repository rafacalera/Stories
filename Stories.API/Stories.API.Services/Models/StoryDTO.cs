using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories.API.Services.Models
{
    public class StoryDTO
    {
        public StoryDTO(int id, string title, string description, int pollId)
        {
            Id = id;
            Title = title;
            Description = description;
            PollId = pollId;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int PollId { get; set; }
    }
}
