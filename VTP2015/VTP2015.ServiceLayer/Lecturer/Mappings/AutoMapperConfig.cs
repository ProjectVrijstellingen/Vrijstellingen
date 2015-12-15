using AutoMapper;
using VTP2015.Entities;
using System.Linq;

namespace VTP2015.ServiceLayer.Lecturer.Mappings
{
    class AutoMapperConfig
    {
        public void Execute()
        {
            Mapper.CreateMap<Entities.Evidence, Models.Evidence>();
            Mapper.CreateMap<File, Models.File>();
            Mapper.CreateMap<Partim, Models.Partim>();
            Mapper.CreateMap<Module, Models.Module>();
            Mapper.CreateMap<Entities.Student, Models.Student>();
            
            Mapper.CreateMap<PartimInformation, Models.PartimInformation>()
                .ForMember(x => x.ModuleName,
                opt => opt.MapFrom(x => x.Module.Name))
                .ForMember(x => x.PartimName,
                opt => opt.MapFrom(x => x.Partim.Name));

            Mapper.CreateMap<Entities.Motivation, Models.Motivation>();
            Mapper.CreateMap<Entities.PrevEducation, Models.PrevEducation>();
        }
    }
}
