using AutoMapper;
using System.Linq;
using VTP2015.Entities;

namespace VTP2015.ServiceLayer.Counselor.Mappings
{
    internal class AutoMapperConfig
    {
        public void Execute()
        {
            Mapper.CreateMap<File, Models.File>(); //TOdo: need to reapply settings
            Mapper.CreateMap<Education, Models.Education>();

            Mapper.CreateMap<Evidence, Models.Evidence>()
                .ForMember(r => r.StudentEmail,
                    opt => opt.MapFrom(r => r.Student.Email));
        }
    }
}
