using MediatR;

namespace Stories.API.Application.Commands.Requests
{
    public class CreateStoryRequest: IRequest<int>
    {
        public CreateStoryRequest(string title, string description, string departament)
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
