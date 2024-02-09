namespace Stories.API.Application.Models.ViewModels
{
    public class VoteViewModel
    {
        public VoteViewModel(bool upVote, int userId)
        {
            UpVote = upVote;
            UserId = userId;
        }

        public bool UpVote { get; set; }
        public int UserId { get; set; }

    }
}
