﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigaApp.Storage.Entities
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [MaxLength(20)]
        public required string Login { get; set; }


        [MaxLength(120)]
        public required byte[] Salt { get; set; }

        [MaxLength(32)]
        public required byte[] PasswordHash { get; set; }


        [InverseProperty(nameof(Topic.Author))]
        public ICollection<Topic>? Topics { get; set; }


        [InverseProperty(nameof(Comment.Author))]
        public ICollection<Comment>? Comments { get; set; }


        [InverseProperty(nameof(Session.User))]
        public ICollection<Session> Sessions { get; set; }
    }
}