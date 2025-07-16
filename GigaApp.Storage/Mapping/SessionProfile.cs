using AutoMapper;
using GigaApp.Storage.Entities;

namespace GigaApp.Storage.Mapping
{
    internal class SessionProfile : Profile
    {
        public SessionProfile()
        {
            CreateMap<Session, Domain.Authentication.Session>()
                .ForMember(d => d.Id, s => s.MapFrom(f => f.SessionId));
        }
    }
}
