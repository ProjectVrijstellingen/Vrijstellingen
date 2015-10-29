using AutoMapper;
using VTP2015.Entities;
using Evidence = System.Security.Policy.Evidence;

namespace VTP2015.ServiceLayer.Lecturer.Mappings
{
    class AutoMapperConfig
    {
        public void Execute()
        {
            Mapper.CreateMap<RequestPartimInformation, Models.RequestPartimInformation>()
                .ForMember(x => x.Module,
                opt => opt.MapFrom(x => x.PartimInformation.Module))
                .ForMember(x => x.Partim,
                opt => opt.MapFrom(x => x.PartimInformation.Partim))
                .ForMember(x => x.Argumentation,
                opt => opt.MapFrom(x => x.Request.Argumentation))
                .ForMember(x => x.File,
                opt => opt.MapFrom(x => x.Request.File));


            Mapper.CreateMap<Evidence, Models.Evidence>();
            Mapper.CreateMap<File, Models.File>();
            Mapper.CreateMap<Partim, Models.Partim>();
            Mapper.CreateMap<Module, Models.Module>();
        }
    }
}
