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

        public bool Like { get; set; }
    }
}
