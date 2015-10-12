using AutoMapper;
using VTP2015.Entities;
using Evidence = System.Security.Policy.Evidence;

namespace VTP2015.ServiceLayer.Lecturer.Mappings
{
    class AutoMapperConfig
    {
        public void Execute()
        {
            Mapper.CreateMap<Request, Models.Request>();
            Mapper.CreateMap<Evidence, Models.Evidence>();
            Mapper.CreateMap<File, Models.File>();
            Mapper.CreateMap<Partim, Models.Partim>();
            Mapper.CreateMap<Module, Models.Module>();
        }
    }
}
