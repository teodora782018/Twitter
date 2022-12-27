﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Net;
using Twitter.API.ActionFilters;
using Twitter.API.Exceptions;
using Twitter.API.Services;
using Twitter.Core.Contracts;
using Twitter.Core.Contracts.V1;
using Twitter.Core.Contracts.V1.Request;

namespace Twitter.API.Controllers.V1
{
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postRepository;
        private ILoggerManager _logger;

        public PostsController(IPostService postRepository, ILoggerManager logger)
        {
            _postRepository = postRepository;
            _logger = logger;
        }

        [HttpGet(ApiRoutes.Post.GetAll)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _postRepository.GetPosts());
        }

        [HttpGet(ApiRoutes.Post.GetPost)]
        public async Task<IActionResult> GetPostById(int id)
        {
            return Ok(await _postRepository.GetPostsById(id));
        }

        [HttpPost(ApiRoutes.Post.Create)]
        [BusinessExceptionFilter(typeof(ValidationRequestException), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
        {
            var post = await _postRepository.CreatePost(postRequest);
            return CreatedAtAction(nameof(Get), new { id = post.Id }, post);
        }

        [HttpPut(ApiRoutes.Post.Update)]
        [BusinessExceptionFilter(typeof(ValidationRequestException), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdatePostRequest postRequest)
        {
            var post = await _postRepository.UpdatePost(postRequest);
            return CreatedAtAction(nameof(Get), new { id = post.Id }, post);
        }

        [HttpDelete(ApiRoutes.Post.Delete)]
        [BusinessExceptionFilter(typeof(ValidationRequestException), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            await _postRepository.DeletePost(id);
            return NoContent();
        }
    }
}