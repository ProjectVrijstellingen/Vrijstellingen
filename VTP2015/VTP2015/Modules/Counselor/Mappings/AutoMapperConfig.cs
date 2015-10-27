using AutoMapper;
using VTP2015.Infrastructure.Tasks;
using VTP2015.Modules.Counselor.ViewModels;
using VTP2015.ServiceLayer.Counselor.Models;

namespace VTP2015.Modules.Counselor.Mappings
{
    public class AutoMapperConfig : IRunAtInit
    {
        public void Execute()
        {
            Mapper.CreateMap<File, FileOverviewViewModel>();
            //.ForMember(m => m.PercentageCompleted,
            //    opt => opt.MapFrom(i => i.Requests.Count != 0 ? (int)((i.Requests.Count(a => a.StatusViewModel != 0) / (i.Requests.Count * 1.0)) * 100) : 0));
            Mapper.CreateMap<Request, RequestDetailViewModel>();
            Mapper.CreateMap<Education, EducationViewModel>();
            Mapper.CreateMap<Evidence, EvidenceViewModel>();
        }
    }
}