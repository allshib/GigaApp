using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Storage.Entities
{
    public class Session
    {
        [Key]
        public Guid SessionId { get; set; }

        public Guid UserId { get; set; }

        public DateTimeOffset ExpiresAt { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
