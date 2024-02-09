namespace Stories.API.Application.Models.Requests
{
    public class VoteRequest
    {
        public VoteRequest(bool upVote, int userId, int storyId)
        {
            UpVote = upVote;
            UserId = userId;
            StoryId = storyId;
        }

        public bool UpVote { get; set; }
        public int UserId { get; set; }
        public int StoryId { get; set; }
    }
}
