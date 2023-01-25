﻿using MediatR;
using Twitter.Core.Contracts.V1;

namespace Twitter.API.Commands
{
    public class DeleteFastPostCommandHandler : IRequestHandler<DeleteFastPostCommand, bool>
    {
        private readonly IFastPostService _service;

        public DeleteFastPostCommandHandler(IFastPostService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeleteFastPostCommand request, CancellationToken cancellationToken)
        {
            var deleted = await _service.DeleteFastPost(request.Id);
            return deleted;
        }
    }
}