using AutoMapper;
using System.Linq;
using VTP2015.Entities;

namespace VTP2015.ServiceLayer.Counselor.Mappings
{
    internal class AutoMapperConfig
    {
        public void Execute()
        {
            Mapper.CreateMap<File, Models.File>()
                .ForMember(r => r.AmountOfRequestsOpen,
                    opt => opt.MapFrom(r => r.Requests.Count))
                .ForMember(r => r.PercentageOfRequestsDone,
                    opt =>
                        opt.MapFrom(
                            i => (int)(i.Requests.SelectMany(x => x.RequestPartimInformations).Count(x => x.Status != Status.Untreated) / (double)i.Requests.SelectMany(x => x.RequestPartimInformations).Count() * 100.0)))
                .ForMember(r => r.Route,
                    opt => opt.MapFrom(r => r.Requests.FirstOrDefault().Name));
            Mapper.CreateMap<Education, Models.Education>();

            Mapper.CreateMap<Evidence, Models.Evidence>()
                .ForMember(r => r.StudentEmail,
                    opt => opt.MapFrom(r => r.Student.Email));
            Mapper.CreateMap<PrevEducation, Models.PrevEducation>()
                 .ForMember(r => r.StudentEmail,
                     opt => opt.MapFrom(r => r.Student.Email));
        }
    }
}
