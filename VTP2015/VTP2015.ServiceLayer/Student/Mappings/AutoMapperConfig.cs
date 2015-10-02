using System;
using AutoMapper;
using VTP2015.Entities;

namespace VTP2015.ServiceLayer.Student.Mappings
{
    public class AutoMapperConfig //todo: disposable
    {
        public void Execute()
        {
            Mapper.CreateMap<Evidence, Models.Evidence>();
            Mapper.CreateMap<File, Models.File>();
            Mapper.CreateMap<PartimInformation, Models.PartimInformation>();
            Mapper.CreateMap<Request, Models.Request>();
            Mapper.CreateMap<Status, Models.Status>();

            Mapper.CreateMap<Models.Evidence, Evidence>();
            Mapper.CreateMap<Models.File, File>();
            Mapper.CreateMap<Models.PartimInformation, PartimInformation>();
            Mapper.CreateMap<Models.Request, Request>();
            Mapper.CreateMap<Models.Status, Status>();
        }

    }
}
