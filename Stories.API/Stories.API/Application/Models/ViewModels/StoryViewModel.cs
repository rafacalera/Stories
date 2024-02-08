
namespace Stories.API.Application.Models.ViewModels
{
    public class StoryViewModel
    {
        public StoryViewModel(int id, string title, string description, string departament)
        {
            Id = id;
            Title = title;
            Description = description;
            Departament = departament;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Departament { get; set; }
        public ICollection<VoteViewModel> Votes { get; set; }


    }
}
