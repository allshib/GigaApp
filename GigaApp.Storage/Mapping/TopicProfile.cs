using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Storage.Mapping
{
    internal class TopicProfile : Profile
    {

        public TopicProfile()
        {
            CreateMap<Topic, Domain.Models.Topic>()
                    .ForMember(d => d.Id, s => s.MapFrom(f => f.TopicId));
            //.ForMember(d=>d.Title, s => s.MapFrom(f => f.Title));
        }
    }
}
