using Stories.API.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories.API.Services.Interfaces
{
    public interface IStoryService
    {
        IEnumerable<StoryDTO> GetAll();
        Task<StoryDTO> GetById(int id);
        Task<int> Add(string title, string description, string departament);
        Task Update(StoryDTO storyDto);
        Task<bool> Delete(int id);
        Task<int> Vote(bool upVote, int storyId, int userId);
    }
}
