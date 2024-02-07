namespace Stories.API.Application.Models.Requests
{
    public class StoryRequest
    {
        public StoryRequest(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public string Title { get; set; }
        public string Description { get; set; }
    }
}
