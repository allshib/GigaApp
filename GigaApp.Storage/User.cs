﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigaApp.Storage
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [MaxLength(20)]
        public required string Login { get; set; }

        [InverseProperty(nameof(Topic.Author))]
        public ICollection<Topic>? Topics { get; set; }


        [InverseProperty(nameof(Comment.Author))]
        public ICollection<Comment>? Comments { get; set; }
    }
}