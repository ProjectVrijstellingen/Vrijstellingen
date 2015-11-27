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
            Mapper.CreateMap<RequestPartimInformation, StudentListViewModel>()
                .ForMember(m => m.StudentId,
                opt => opt.MapFrom(i => i.Student.Id))
                .ForMember(m => m.Name,
                opt => opt.MapFrom(i => i.Student.Name))
                .ForMember(m => m.FirstName,
                opt => opt.MapFrom(i => i.Student.FirstName))
                .ForMember(m => m.Email,
                opt => opt.MapFrom(i => i.Student.Email));

            Mapper.CreateMap<RequestPartimInformation, PartimListViewModel>()
                .ForMember(m => m.PartimId,
                opt => opt.MapFrom(i => i.Partim.Code))
                .ForMember(m => m.PartimName,
                opt => opt.MapFrom(i => i.Partim.Name));

            Mapper.CreateMap<RequestPartimInformation, RequestListViewModel>()
                //.ForMember(m => m.StudentId,
                //.ForMember(m => m.Student,
                //    opt => opt.MapFrom(i => i.Student))
                .ForMember(m => m.PartimName,
                    opt => opt.MapFrom(i => i.Partim.Name))
                .ForMember(m => m.ModuleName,
                    opt => opt.MapFrom(i => i.Module.Name))
                .ForMember(m => m.Id,
                    opt => opt.MapFrom(i => i.Id))
                .ForMember(m => m.Argumentation,
                    opt => opt.MapFrom(i => i.Argumentation))
                .ForMember(m => m.Evidence,
                    opt => opt.MapFrom(i => i.Evidence));

            Mapper.CreateMap<Evidence, EvidenceViewModel>();
            //    .ForMember(m => m.StudentEmail,
            //        opt => opt.MapFrom(i => i.Student.Email));

            Mapper.CreateMap<PartimInformation, PartimListViewModel>();
            Mapper.CreateMap<ServiceLayer.Lecturer.Models.Student, StudentListViewModel>();
        }
    }
}