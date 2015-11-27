using AutoMapper;
using VTP2015.Entities;
using System.Linq;

namespace VTP2015.ServiceLayer.Lecturer.Mappings
{
    class AutoMapperConfig
    {
        public void Execute()
        {
            //Mapper.CreateMap<RequestPartimInformation, Models.RequestPartimInformation>()
            //    .ForMember(x => x.Module,
            //    opt => opt.MapFrom(x => x.PartimInformation.Module))
            //    .ForMember(x => x.Partim,
            //    opt => opt.MapFrom(x => x.PartimInformation.Partim))
            //    .ForMember(x => x.Argumentation,
            //    opt => opt.MapFrom(x => x.Request.Argumentation))
            //    .ForMember(x => x.Evidence,
            //    opt => opt.MapFrom(x => x.Request.Evidence.AsQueryable()))
            //    .ForMember(x => x.Student,
            //    opt => opt.MapFrom(x => x.Request.File.Student))
            //    .ForMember(x => x.Status,
            //    opt => opt.MapFrom(x => (Models.Status)(int)x.Status));

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

        }
    }
}
