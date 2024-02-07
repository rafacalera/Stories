namespace Stories.API.Application.Models.ViewModels
{
    public class StoryViewModel
    {
        public StoryViewModel(string title, string description, int pollId)
        {
            Title = title;
            Description = description;
            PollId = pollId;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public int PollId { get; set; }
    }
}
