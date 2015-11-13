using System;
using System.Linq;
using AutoMapper;
using AutoMapper.Internal;
using AutoMapper.Mappers;
using VTP2015.Entities;

namespace VTP2015.ServiceLayer.Student.Mappings
{
    public class AutoMapperConfig 
    {
        public void Execute()
        {
            Mapper.CreateMap<File, Models.File>()
                .ForMember(r => r.Education, opt => opt.MapFrom(r => r.Education.Name))
                .ForMember(r => r.StudentMail, opt => opt.MapFrom(r => r.Student.Email))
                .ForMember(x => x.FileStatus, opt => opt.MapFrom(x => (Models.FileStatus)(int)x.FileStatus));
            Mapper.CreateMap<PartimInformation, Models.PartimInformation>()
                .ForMember(x => x.Code, ops => ops.MapFrom(x => x.Module.Code))
                .ForMember(x => x.TotalCount, ops => ops.MapFrom(x => x.Module.PartimInformation.Count))
                .ForMember(x => x.Status, opt => opt.UseValue(Models.Status.Empty));
            Mapper.CreateMap<Evidence, Models.Evidence>();
            Mapper.CreateMap<RequestPartimInformation, Models.PartimInformation>()
                .ForMember(x => x.SuperCode, opt => opt.MapFrom(x => x.PartimInformation.SuperCode))
                .ForMember(x => x.Code, opt => opt.MapFrom(x => x.PartimInformation.Module.Code))
                .ForMember(x => x.ModuleName, opt => opt.MapFrom(x => x.PartimInformation.Module.Name))
                .ForMember(x => x.PartimName, opt => opt.MapFrom(x => x.PartimInformation.Partim.Name))
                .ForMember(x => x.TotalCount, opt => opt.MapFrom(x => x.PartimInformation.Module.PartimInformation.Count))
                .ForMember(x => x.Status,opt => opt.MapFrom(x => (Models.Status)(int)x.Status));

            Mapper.CreateMap<Models.Evidence, Evidence>();
            Mapper.CreateMap<Models.File, File>();
            Mapper.CreateMap<Models.PartimInformation, PartimInformation>();
            Mapper.CreateMap<Models.Request, Request>();
        }

    }
}
