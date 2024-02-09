using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories.API.Services.Interfaces
{
    public interface IVoteService
    {
        Task<bool> Add(bool upVote, int storyId, int userId);
        Task<bool> Delete(int id);
    }
}
