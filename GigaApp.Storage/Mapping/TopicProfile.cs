using AutoMapper;
using GigaApp.Storage.Entities;

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
