using MediatR;


namespace Stories.API.Application.Commands.Requests
{
    public class DeleteStoryRequest : IRequest<bool>
    {
        public DeleteStoryRequest(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
