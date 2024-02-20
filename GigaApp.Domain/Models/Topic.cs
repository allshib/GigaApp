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
        public string Title { get; set; }    
        public DateTimeOffset UpdatedAt { get; set; }
        public string Author { get; set; }
    }
}
