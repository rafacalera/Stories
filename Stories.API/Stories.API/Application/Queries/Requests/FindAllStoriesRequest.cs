using MediatR;
using Stories.API.Application.Queries.Responses;

namespace Stories.API.Application.Queries.Requests
{
    public class FindAllStoriesRequest : IRequest<List<FindStoryResponse>>
    {
    }
}
