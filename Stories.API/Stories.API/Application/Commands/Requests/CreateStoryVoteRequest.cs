using MediatR;

namespace Stories.API.Application.Commands.Requests
{
    public class CreateStoryVoteRequest : IRequest<bool?>
    {
        public CreateStoryVoteRequest(bool upVote, int storyId, int userId)
        {
            UpVote = upVote;
            StoryId = storyId;
            UserId = userId;
        }

        public bool UpVote { get; set; }
        public int StoryId { get; set; }
        public int UserId { get; set; }
    }
}
