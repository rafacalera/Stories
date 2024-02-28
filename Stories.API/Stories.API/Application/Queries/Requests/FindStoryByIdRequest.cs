using MediatR;
using Stories.API.Application.Queries.Responses;

namespace Stories.API.Application.Queries.Requests
{
    public class FindStoryByIdRequest : IRequest<FindStoryResponse>
    {
        public FindStoryByIdRequest(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
