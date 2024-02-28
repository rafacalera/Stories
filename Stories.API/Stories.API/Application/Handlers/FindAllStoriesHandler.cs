using MediatR;
using Stories.API.Application.Models.ViewModels;
using Stories.API.Application.Queries.Requests;
using Stories.API.Application.Queries.Responses;
using Stories.API.Services.Interfaces;
using System.Linq;

namespace Stories.API.Application.Handlers
{
    public class FindAllStoriesHandler : IRequestHandler<FindAllStoriesRequest, List<FindStoryResponse>>
    {
        private readonly IStoryService _service;

        public FindAllStoriesHandler(IStoryService service)
        {
            _service = service;
        }

        public Task<List<FindStoryResponse>> Handle(FindAllStoriesRequest request, CancellationToken cancellationToken)
        {
            var stories = _service.GetAll();

            var response = stories.Select(s =>
                new FindStoryResponse(s.Id, s.Title, s.Description, s.Departament,
                s.Votes.Select(v => new VoteResponse(v.Id, v.UpVote, v.UserId))
                    .ToList()))
                    .ToList();

            return Task.FromResult(response);
        }
    }
}

