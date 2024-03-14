﻿using AutoMapper;
using GigaApp.Storage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Storage.Mapping
{
    internal class ForumProfile : Profile
    {

        public ForumProfile() {
            CreateMap<Forum, Domain.Models.Forum>()
                    .ForMember(d => d.Id, s => s.MapFrom(f => f.ForumId));


            CreateMap<Domain.Models.Forum, Forum>()
                .ForMember(d => d.ForumId, s => s.MapFrom(f => f.Id));
        }
    }
}
