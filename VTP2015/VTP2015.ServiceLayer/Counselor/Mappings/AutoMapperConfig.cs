using System.Linq;
using AutoMapper;
using VTP2015.Entities;

namespace VTP2015.ServiceLayer.Counselor.Mappings
{
    class AutoMapperConfig
    {
        public void Execute()
        {
            Mapper.CreateMap<File, Models.File>()
                .ForMember(r => r.AmountOfRequestsOpen,
                    opt => opt.MapFrom(r => r.Requests));
            Mapper.CreateMap<Education, Models.Education>();
            Mapper.CreateMap<RequestPartimInformation, Models.RequestPartimInformation>()
                .ForMember(r => r.Status,
                    opt => opt.MapFrom(r => (Models.Status)r.Status));
        }
    }
}
