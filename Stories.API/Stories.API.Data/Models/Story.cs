using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories.API.Data.Models
{
    public class Story
    {
         public Story(string title, string description, string departament)
        {
            Title = title;
            Description = description;
            Departament = departament;
        }

        public int Id { get; private set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Departament { get; set; }
        public ICollection<Vote> Votes { get; } = new List<Vote>();

    }
}
