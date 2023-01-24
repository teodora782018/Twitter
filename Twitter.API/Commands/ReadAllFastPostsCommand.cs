﻿using MediatR;
using Twitter.Core.Domain.Entities;

namespace Twitter.API.Commands
{
    public class ReadAllFastPostsCommand : IRequest<List<FastPost>>
    {
    }
}
