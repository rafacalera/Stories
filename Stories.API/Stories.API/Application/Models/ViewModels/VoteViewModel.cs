namespace Stories.API.Application.Models.ViewModels
{
    public class VoteViewModel
    {
        public VoteViewModel(int id, bool upVote, int userId)
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
