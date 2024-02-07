namespace Stories.API.Application.Models.Requests
{
    public class StoryRequest
    {
        public StoryRequest(string title, string description, string departament)
        {
            Title = title;
            Description = description;
            Departament = departament;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Departament { get; set; }
    }
}
