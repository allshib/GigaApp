using AutoMapper;
using GigaApp.API.Models;

namespace GigaApp.API.Mapping
{
    public class ApiProfile : Profile
    {

        public ApiProfile()
        {
            CreateMap<Domain.Models.Forum, Forum>();
            CreateMap<Domain.Models.Topic, Topic>();

        }
    }
}
