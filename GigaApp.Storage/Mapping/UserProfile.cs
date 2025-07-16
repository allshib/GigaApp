using AutoMapper;
using GigaApp.Domain.Authentication;

namespace GigaApp.Storage.Mapping
{
    public class UserProfile : Profile
    {

        public UserProfile()
        {
            CreateMap<Entities.User, RecognizedUser>();
        }
    }
}
