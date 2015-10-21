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
            //Mapper.CreateMap<Request, StudentListViewModel>()
            //    .ForMember(m => m.StudentId,
            //        opt => opt.MapFrom(i => i.FileName.StudentId))
            //    .ForMember(m => m.Name,
            //        opt => opt.MapFrom(i => i.FileName.Student.Name))
            //    .ForMember(m => m.Prename,
            //        opt => opt.MapFrom(i => i.FileName.Student.FirstName));

            //Mapper.CreateMap<Request, RequestListViewModel>()
            //    .ForMember(m => m.StudentId,
            //        opt => opt.MapFrom(i => i.FileName.StudentId))
            //    .ForMember(m => m.PartimName,
            //        opt => opt.MapFrom(i => i.PartimInformation.Partim.Name))
            //    .ForMember(m => m.ModuleName,
            //        opt => opt.MapFrom(i => i.PartimInformation.Module.Name));

            //Mapper.CreateMap<Evidence, EvidenceViewModel>()
            //    .ForMember(m => m.StudentEmail,
            //        opt => opt.MapFrom(i => i.Student.Email));
        }
    }
}