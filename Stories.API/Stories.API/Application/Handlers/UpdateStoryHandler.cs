using MediatR;
using Stories.API.Application.Commands.Requests;
using Stories.API.Application.Models.Requests;
using Stories.API.Application.Models.ViewModels;
using Stories.API.Services.Interfaces;
using Stories.API.Services.Models;

namespace Stories.API.Application.Handlers
{
    public class UpdateStoryHandler : IRequestHandler<UpdateStoryRequest, bool?>
    {
        private readonly IStoryService _service;

        public UpdateStoryHandler(IStoryService service)
        {
            _service = service;
        }

        public async Task<bool?> Handle(UpdateStoryRequest request, CancellationToken cancellationToken)
        {
            try 
            { 
                await _service.Update(new StoryDTO(request.Id, request.Title, request.Description, request.Departament));
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
