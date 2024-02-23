﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigaApp.Storage
{
    public class Comment
    {
        [Key]
        public Guid CommentId { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }


        [MaxLength(2500)]
        public required string Text { get; set; }

        public Guid UserId { get; set; }
        public Guid TopicId { get; set; }



        [ForeignKey(nameof(UserId))]
        public User Author { get; set; }



        [ForeignKey(nameof(TopicId))]
        public Topic Topic { get; set; }
    }
}