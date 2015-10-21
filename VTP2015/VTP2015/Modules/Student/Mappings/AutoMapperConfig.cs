using AutoMapper;
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
            Mapper.CreateMap<Evidence, EvidenceListViewModel>();
            Mapper.CreateMap<PartimInformation, PartimViewModel>();
            Mapper.CreateMap<Request, RequestDetailViewModel>();
        }
    }
}