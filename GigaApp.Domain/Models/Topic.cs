using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Models
{
    public class Topic
    {
        public Guid Id { get; set; }   
        public Guid ForumId { get; set; }
        public string Title { get; set; }    
        public DateTimeOffset UpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public Guid UserId { get; set; }
    }
}
