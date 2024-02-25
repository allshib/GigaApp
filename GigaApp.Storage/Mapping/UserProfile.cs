using AutoMapper;
using GigaApp.Domain.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Storage.Mapping
{
    public class UserProfile : Profile
    {

        public UserProfile()
        {
            CreateMap<User, RecognizedUser>();
        }
    }
}
