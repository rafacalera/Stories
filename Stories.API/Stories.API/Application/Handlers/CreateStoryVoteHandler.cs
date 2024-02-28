using Azure.Core;
using MediatR;
using Stories.API.Application.Commands.Requests;
using Stories.API.Application.Models.Requests;
using Stories.API.Application.Models.ViewModels;
using Stories.API.Services.Interfaces;

namespace Stories.API.Application.Handlers
{
    public class CreateStoryVoteHandler : IRequestHandler<CreateStoryVoteRequest, bool?>
    {
        private readonly IStoryService _service;

        public CreateStoryVoteHandler(IStoryService service)
        {
            _service = service;
        }

        public async Task<bool?> Handle(CreateStoryVoteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _service.Vote(request.UpVote, request.StoryId, request.UserId);

                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }
    }
}
