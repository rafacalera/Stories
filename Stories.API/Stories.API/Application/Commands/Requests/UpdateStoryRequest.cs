using MediatR;

namespace Stories.API.Application.Commands.Requests
{
    public class UpdateStoryRequest : IRequest<bool?>
    {
        public UpdateStoryRequest(int id, string title, string description, string departament)
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
    }
}
