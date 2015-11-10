using AutoMapper;
using VTP2015.Infrastructure.Tasks;
using VTP2015.Modules.Lecturer.ViewModels;
using VTP2015.ServiceLayer.Lecturer.Models;

namespace VTP2015.Modules.Lecturer.Mappings
{
    public class AutoMapperConfig : IRunAtInit
    {
        public void Execute()
        {
            Mapper.CreateMap<RequestPartimInformation, StudentListViewModel>();

            Mapper.CreateMap<RequestPartimInformation, RequestListViewModel>();
            //    .ForMember(m => m.StudentId,
            //        opt => opt.MapFrom(i => i.FileName.StudentId))
            //    .ForMember(m => m.PartimName,
            //        opt => opt.MapFrom(i => i.PartimInformation.Partim.Name))
            //    .ForMember(m => m.ModuleName,
            //        opt => opt.MapFrom(i => i.PartimInformation.Module.Name));

            Mapper.CreateMap<Evidence, EvidenceViewModel>();
            //    .ForMember(m => m.StudentEmail,
            //        opt => opt.MapFrom(i => i.Student.Email));

        }
    }
}