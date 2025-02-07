using AutoMapper;
using SomeAPIEFCore.Data.Entities;
using SomeAPIEFCore.DTO;

namespace SomeAPIEFCore.Data.Mappings
{
    public class AnyMappingProfile : Profile
    {

        public AnyMappingProfile() 
        {
            CreateMap<RoadMap, RoadMapDTO>();
            CreateMap<RoadMapElement, RoadMapElementDTO>()
                .ForMember(i => i.EditDate, 
                opt => opt.MapFrom(i => i.EditDate.ToString("Редакция от dd MMMM yyyy г.")));

            
        }

    }
}
