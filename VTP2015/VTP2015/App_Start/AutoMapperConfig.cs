using System;
using AutoMapper;
using System.Linq;
using VTP2015.Entities;
using VTP2015.Infrastructure.Tasks;
using Student = VTP2015.ViewModels.Student;
using TrajectBegeleider = VTP2015.ViewModels.TrajectBegeleider;
using Docent = VTP2015.ViewModels.Docent;

namespace VTP2015
{
    public class AutoMapperConfig : IRunAtInit
    {
        public void Execute()
        {
            //Student
            Mapper.CreateMap<File, Student.DossierViewModel>();
            Mapper.CreateMap<File, Student.DossierListViewModel>();
            Mapper.CreateMap<Evidence, Student.BewijsListViewModel>();
            Mapper.CreateMap<PartimInformation, Student.PartimViewModel>()
                .ForMember(m => m.ModuleId,
                    opt => opt.MapFrom(i => i.Module.Id))
                .ForMember(m => m.ModuleNaam,
                    opt => opt.MapFrom(i => i.Module.Name))
                .ForMember(m => m.PartimNaam,
                    opt => opt.MapFrom(i => i.Partim.Name));
            Mapper.CreateMap<Request, Student.AanvraagDetailViewModel>();

            //Counselor
            Mapper.CreateMap<File, TrajectBegeleider.DossierOverviewViewModel>()
                .ForMember(m => m.PercentageVoltooid,
                    opt => opt.MapFrom(i => i.Requests.Count != 0 ? (int)((i.Requests.Count(a => a.Status != 0) / (i.Requests.Count * 1.0)) * 100) : 0));
            Mapper.CreateMap<Request, TrajectBegeleider.AanvraagDetailsViewModel>();
            Mapper.CreateMap<Education, TrajectBegeleider.OpleidingViewModel>();


            //Lecturer
            Mapper.CreateMap<Request, Docent.StudentListViewModel>()
                .ForMember(m => m.StudentId,
                    opt => opt.MapFrom(i => i.File.StudentId))
                .ForMember(m => m.Naam,
                    opt => opt.MapFrom(i => i.File.Student.Name))
                .ForMember(m => m.Voornaam,
                    opt => opt.MapFrom(i => i.File.Student.FirstName));

            Mapper.CreateMap<Request, Docent.AanvraagListViewModel>()
                .ForMember(m => m.StudentId,
                    opt => opt.MapFrom(i => i.File.StudentId))
                .ForMember(m => m.PartimName,
                    opt => opt.MapFrom(i => i.PartimInformation.Partim.Name))
                .ForMember(m => m.ModuleName,
                    opt => opt.MapFrom(i => i.PartimInformation.Module.Name));

            Mapper.CreateMap<Evidence, Docent.BewijsViewModel>()
                .ForMember(m => m.StudentEmail,
                    opt => opt.MapFrom(i => i.Student.Email));


        }
    }
}