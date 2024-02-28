using MediatR;
using Stories.API.Application.Commands.Requests;
using Stories.API.Application.Models.Requests;
using Stories.API.Application.Models.ViewModels;
using Stories.API.Services.Interfaces;

namespace Stories.API.Application.Handlers
{
    public class CreateStoryHandler : IRequestHandler<CreateStoryRequest, int>
    {
        private readonly IStoryService _service;

        public CreateStoryHandler(IStoryService service)
        {
            _service = service;
        }

        public async Task<int> Handle(CreateStoryRequest request, CancellationToken cancellationToken)
        {
         
                try
                {
                    int id = await _service.Add(request.Title, request.Description, request.Departament);
                    return id;
                }
                catch (ArgumentException)
                {
                    return 0;
                }
            
        }
    }
}
