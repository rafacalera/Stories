using MediatR;
using Stories.API.Application.Models.ViewModels;
using Stories.API.Application.Queries.Requests;
using Stories.API.Application.Queries.Responses;
using Stories.API.Services.Interfaces;

namespace Stories.API.Application.Handlers
{
    public class FindStoryByIdHandler : IRequestHandler<FindStoryByIdRequest, FindStoryResponse>
    {
        private readonly IStoryService _service;

        public FindStoryByIdHandler(IStoryService service)
        {
            _service = service;
        }

        public async Task<FindStoryResponse?> Handle(FindStoryByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var storyDto = await _service.GetById(request.Id);

                return new FindStoryResponse(storyDto.Id, storyDto.Title, storyDto.Description, storyDto.Departament, 
                    storyDto.Votes
                    .Select(s => new VoteResponse(s.Id, s.UpVote, s.UserId))
                    .ToList());
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }
    }
}
