﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories.API.Data.Models
{
    public class Story
    {
        public int Id { get; private set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int VotingId { get; set; }
    }
}
