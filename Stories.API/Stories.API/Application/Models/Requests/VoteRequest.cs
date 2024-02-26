namespace Stories.API.Application.Models.Requests
{
    public class VoteRequest
    {
        public VoteRequest(bool upVote, int userId)
        {
            UpVote = upVote;
            UserId = userId;
        }

        public bool UpVote { get; set; }
        public int UserId { get; set; }
    }
}
