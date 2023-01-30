﻿using FluentResults;
using Microsoft.EntityFrameworkCore;
using Twitter.API.Exceptions;
using Twitter.Core.Contracts.V1;
using Twitter.Core.Domain.Entities;
using Twitter.Infrastructure.Data;

namespace Twitter.API.Services
{
    public class FastPostService : IFastPostService
    {
        private readonly TwitterAPIContext _context;

        public FastPostService(TwitterAPIContext context)
        {
            _context = context;
        }

        public async Task<Result<FastPost>> CreateFastPost(FastPost fastPostRequest)
        {
            if (await _context.Categories.FindAsync(fastPostRequest.CategoryId) == null)
            {
                return Result.Fail(new Error("Category with Id: '" + fastPostRequest.CategoryId + "' not found"));
            }

            fastPostRequest.Created = DateTime.Now;

            await _context.FastPosts.AddAsync(fastPostRequest);
            await _context.SaveChangesAsync();
            return Result.Ok(fastPostRequest);
        }

        public async Task<FastPost> ReadFastPost(int id)
        {
            var time = DateTime.Now.AddHours(-24);
            var fastPost = await _context.FastPosts.FirstOrDefaultAsync(p => p.Id == id && p.Created > time);

            if (fastPost == null)
            {
                throw new ValidationRequestException("Tweet with Id: '" + id + "' not found!");
            }

            return fastPost;
        }

        public async Task<List<FastPost>> ReadAllFastPosts()
        {
            var time = DateTime.Now.AddHours(-24);
            return await _context.FastPosts.Where(p => p.Created > time).ToListAsync();
        }

        public async Task<bool> DeleteFastPost(int id)
        {
            var time = DateTime.Now.AddHours(-24);
            var fastPost = await _context.FastPosts.FirstOrDefaultAsync(p => p.Id == id && p.Created > time);

            if (fastPost == null)
            {
                throw new ValidationRequestException("Tweet with Id: '" + id + "' not found!");
            }
            _context.FastPosts.Remove(fastPost);
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }
    }
}
