using AutoMapper;

namespace GigaApp.XAF.Module.Mapping;

public class XafObjectsProfile : Profile
{
    public XafObjectsProfile()
    {
        CreateMap<Domain.Models.Forum, XAF.Module.BusinessObjects.Forum>();
        CreateMap<XAF.Module.BusinessObjects.Forum, Domain.Models.Forum>();


    }
}