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
            Mapper.CreateMap<BusinessModels.File, ViewModels.FileOverviewViewModel>();
            Mapper.CreateMap<BusinessModels.Request, ViewModels.RequestDetailViewModel>();
            Mapper.CreateMap<BusinessModels.Module, ViewModels.Models.Module>();
            Mapper.CreateMap<BusinessModels.Partim, ViewModels.Models.Partim>()
                .ForMember(opt => opt.Status,
                    src => src.MapFrom(r => (StatusViewModel)r.Status));

            Mapper.CreateMap<BusinessModels.Education, ViewModels.EducationViewModel>();
            Mapper.CreateMap<BusinessModels.Evidence, ViewModels.Models.Evidence>();
        }
    }
}