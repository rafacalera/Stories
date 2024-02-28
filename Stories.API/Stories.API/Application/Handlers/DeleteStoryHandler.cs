using MediatR;
using Stories.API.Application.Commands.Requests;
using Stories.API.Services.Interfaces;
using System;

namespace Stories.API.Application.Handlers
{
    public class DeleteStoryHandler : IRequestHandler<DeleteStoryRequest, bool>
    {
        private readonly IStoryService _service;

        public DeleteStoryHandler(IStoryService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeleteStoryRequest request, CancellationToken cancellationToken)
        {
            return await _service.Delete(request.Id);
        }
    }
}
