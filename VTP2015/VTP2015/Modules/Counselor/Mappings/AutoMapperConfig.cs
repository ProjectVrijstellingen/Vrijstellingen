using AutoMapper;
using VTP2015.Infrastructure.Tasks;
using VTP2015.Modules.Counselor.ViewModels;
using VTP2015.ServiceLayer.Counselor.Models;
using Evidence = VTP2015.ServiceLayer.Counselor.Models.Evidence;
using File = VTP2015.ServiceLayer.Counselor.Models.File;

namespace VTP2015.Modules.Counselor.Mappings
{
    public class AutoMapperConfig : IRunAtInit
    {
        public void Execute()
        {
            Mapper.CreateMap<File, FileOverviewViewModel>();
            Mapper.CreateMap<Request, RequestDetailViewModel>();
            Mapper.CreateMap<Education, EducationViewModel>();
            Mapper.CreateMap<Evidence, EvidenceViewModel>();
        }
    }
}