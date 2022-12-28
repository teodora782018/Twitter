﻿namespace Twitter.Core.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public DateTime Created { get; set; }
        public int CategoryId { get; set; }
        public string CreatedById { get; set; }
    }
}
