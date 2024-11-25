using Dtos.Tags;
using Models;
using Profile = AutoMapper.Profile;

namespace Business;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<Tag, TagVm>();
        //.ForMember();
    }
}