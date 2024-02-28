namespace Stories.API.Application.Queries.Responses
{
    public class VoteResponse
    {
        public VoteResponse(int id, bool upVote, int userId)
        {
            Id = id;
            UpVote = upVote;
            UserId = userId;
        }

        public int Id { get; set; }
        public bool UpVote { get; set; }
        public int UserId { get; set; }
    }
}
