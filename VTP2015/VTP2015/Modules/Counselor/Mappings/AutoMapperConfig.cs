using AutoMapper;
using VTP2015.Infrastructure.Tasks;
using VTP2015.Modules.Counselor.ViewModels;
using BusinessModels = VTP2015.ServiceLayer.Counselor.Models;

namespace VTP2015.Modules.Counselor.Mappings
{
    public class AutoMapperConfig : IRunAtInit
    {
        public void Execute()
        {
            Mapper.CreateMap<BusinessModels.File, FileOverviewViewModel>();
            Mapper.CreateMap<BusinessModels.Request, RequestDetailViewModel>();
            Mapper.CreateMap<BusinessModels.Module, ViewModels.Models.Module>();
            Mapper.CreateMap<BusinessModels.Partim, ViewModels.Models.Partim>()
                .ForMember(opt => opt.Status,
                    src => src.MapFrom(r => (StatusViewModel)r.Status));

            Mapper.CreateMap<BusinessModels.Education, EducationViewModel>();
            Mapper.CreateMap<BusinessModels.Evidence, ViewModels.Models.Evidence>();
            Mapper.CreateMap<BusinessModels.PartimInformation, PartimInformationViewModel>();
        }
    }
}