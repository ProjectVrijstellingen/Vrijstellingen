using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using VTP2015.Infrastructure.Tasks;
using VTP2015.Modules.Student.ViewModels;
using VTP2015.ServiceLayer.Student.Models;

namespace VTP2015.Modules.Student.Mappings
{
    public class AutoMapperConfig : IRunAtInit
    {
        public void Execute()
        {
            Mapper.CreateMap<File, FileViewModel>();
            Mapper.CreateMap<File, FileListViewModel>();
            Mapper.CreateMap<PrevEducation, EducationListViewModel>();
            Mapper.CreateMap<Evidence, EvidenceListViewModel>();
            Mapper.CreateMap<PartimInformation, PartimViewModel>()
                .ForMember(x => x.Status, opt => opt.MapFrom(x => (int)x.Status));
            Mapper.CreateMap<Request, RequestDetailViewModel>();
            Mapper.CreateMap<ServiceLayer.Student.Models.Student, StudentViewModel>();
        }
    }
}