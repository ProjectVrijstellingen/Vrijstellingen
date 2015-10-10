using AutoMapper;
using VTP2015.Infrastructure.Tasks;
using VTP2015.Modules.Counselor.ViewModels;
using VTP2015.Modules.Lecturer.ViewModels;
using VTP2015.Modules.Student.ViewModels;
using Student =  VTP2015.ServiceLayer.Student.Models;
using Counselor = VTP2015.ServiceLayer.Counselor.Models;
using Lecturer = VTP2015.ServiceLayer.Lecturer.Models;


namespace VTP2015
{
    public class AutoMapperConfig : IRunAtInit
    {
        public void Execute()
        {
            //Student
            Mapper.CreateMap<Student.File, FileViewModel>();
            Mapper.CreateMap<Student.File, FileListViewModel>();
            Mapper.CreateMap<Student.Evidence, EvidenceListViewModel>();
            Mapper.CreateMap<Student.PartimInformation, PartimViewModel>();
            Mapper.CreateMap<Student.Request, RequestDetailViewModel>();

            ////Counselor
            //Mapper.CreateMap<Counselor.FileName, DossierOverviewViewModel>()
            //    .ForMember(m => m.PercentageVoltooid,
            //        opt => opt.MapFrom(i => i.Requests.Count != 0 ? (int)((i.Requests.Count(a => a.Status != 0) / (i.Requests.Count * 1.0)) * 100) : 0));
            //Mapper.CreateMap<Counselor.Request, AanvraagDetailsViewModel>();
            //Mapper.CreateMap<Counselor.Education, OpleidingViewModel>();


            ////Lecturer
            //Mapper.CreateMap<Lecturer.Request, StudentListViewModel>()
            //    .ForMember(m => m.StudentId,
            //        opt => opt.MapFrom(i => i.FileName.StudentId))
            //    .ForMember(m => m.Naam,
            //        opt => opt.MapFrom(i => i.FileName.Student.Name))
            //    .ForMember(m => m.Voornaam,
            //        opt => opt.MapFrom(i => i.FileName.Student.FirstName));

            //Mapper.CreateMap<Lecturer.Request, AanvraagListViewModel>()
            //    .ForMember(m => m.StudentId,
            //        opt => opt.MapFrom(i => i.FileName.StudentId))
            //    .ForMember(m => m.PartimName,
            //        opt => opt.MapFrom(i => i.PartimInformation.Partim.Name))
            //    .ForMember(m => m.ModuleName,
            //        opt => opt.MapFrom(i => i.PartimInformation.Module.Name));

            //Mapper.CreateMap<Lecturer.Evidence, BewijsViewModel>()
            //    .ForMember(m => m.StudentEmail,
            //        opt => opt.MapFrom(i => i.Student.Email));


        }
    }
}