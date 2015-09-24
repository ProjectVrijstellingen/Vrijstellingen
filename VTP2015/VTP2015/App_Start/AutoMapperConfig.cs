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
            Mapper.CreateMap<Dossier, Student.DossierViewModel>();
            Mapper.CreateMap<Dossier, Student.DossierListViewModel>();
            Mapper.CreateMap<Bewijs, Student.BewijsListViewModel>();
            Mapper.CreateMap<PartimInformatie, Student.PartimViewModel>()
                .ForMember(m => m.ModuleId,
                    opt => opt.MapFrom(i => i.Module.ModuleId))
                .ForMember(m => m.ModuleNaam,
                    opt => opt.MapFrom(i => i.Module.Naam))
                .ForMember(m => m.PartimNaam,
                    opt => opt.MapFrom(i => i.Partim.Naam));
            Mapper.CreateMap<Aanvraag, Student.AanvraagDetailViewModel>();

            //TrajectBegeleider
            Mapper.CreateMap<Dossier, TrajectBegeleider.DossierOverviewViewModel>()
                .ForMember(m => m.PercentageVoltooid,
                    opt => opt.MapFrom(i => i.Aanvragen.Count != 0 ? (int)((i.Aanvragen.Count(a => a.Status != 0) / (i.Aanvragen.Count * 1.0)) * 100) : 0));
            Mapper.CreateMap<Aanvraag, TrajectBegeleider.AanvraagDetailsViewModel>();
            Mapper.CreateMap<Opleiding, TrajectBegeleider.OpleidingViewModel>();


            //Docent
            Mapper.CreateMap<Aanvraag, Docent.StudentListViewModel>()
                .ForMember(m => m.StudentId,
                    opt => opt.MapFrom(i => i.Dossier.StudentId))
                .ForMember(m => m.Naam,
                    opt => opt.MapFrom(i => i.Dossier.Student.Naam))
                .ForMember(m => m.Voornaam,
                    opt => opt.MapFrom(i => i.Dossier.Student.VoorNaam));

            Mapper.CreateMap<Aanvraag, Docent.AanvraagListViewModel>()
                .ForMember(m => m.StudentId,
                    opt => opt.MapFrom(i => i.Dossier.StudentId))
                .ForMember(m => m.PartimName,
                    opt => opt.MapFrom(i => i.PartimInformatie.Partim.Naam))
                .ForMember(m => m.ModuleName,
                    opt => opt.MapFrom(i => i.PartimInformatie.Module.Naam));

            Mapper.CreateMap<Bewijs, Docent.BewijsViewModel>()
                .ForMember(m => m.StudentEmail,
                    opt => opt.MapFrom(i => i.Student.Email));


        }
    }
}