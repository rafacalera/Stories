using Stories.API.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories.API.Services.Models
{
    internal class UserDTO
    {
        public UserDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; set; }
        public ICollection<Vote> Votes { get; } = new List<Vote>();

    }
}
