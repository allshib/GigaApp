using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

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
