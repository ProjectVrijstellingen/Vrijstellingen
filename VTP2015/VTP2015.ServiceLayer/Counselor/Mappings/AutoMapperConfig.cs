using AutoMapper;
using VTP2015.Entities;

namespace VTP2015.ServiceLayer.Counselor.Mappings
{
    class AutoMapperConfig
    {
        public void Execute()
        {
            Mapper.CreateMap<File, Models.File>();
            Mapper.CreateMap<Education, Models.Education>();
            Mapper.CreateMap<Request, Models.Request>();
        }
    }
}
