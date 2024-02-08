using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories.API.Data.Models
{
    public class User
    {
        public User(string name)
        {
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; set; }
        public ICollection<Vote> Votes { get; } = new List<Vote>();
    }
}
