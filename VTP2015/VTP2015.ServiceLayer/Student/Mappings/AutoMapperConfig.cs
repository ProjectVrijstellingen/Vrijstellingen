﻿using System;
using AutoMapper;
using VTP2015.Entities;

namespace VTP2015.ServiceLayer.Student.Mappings
{
    public class AutoMapperConfig 
    {
        public void Execute()
        {            
            Mapper.CreateMap<File, Models.File>()
                .ForMember(r => r.Education,
                    opt => opt.MapFrom(r => r.Education.Name))
                .ForMember(r => r.StudentMail,
                    opt => opt.MapFrom(r => r.Student.Email));

            Mapper.CreateMap<PartimInformation, Models.PartimInformation>()
                .ForMember(x => x.Code,
                ops => ops.MapFrom(x => x.Module.Code));
            Mapper.CreateMap<Evidence, Models.Evidence>();

            Mapper.CreateMap<Models.Evidence, Evidence>();
            Mapper.CreateMap<Models.File, File>();
            Mapper.CreateMap<Models.PartimInformation, PartimInformation>();
            Mapper.CreateMap<Models.Request, Request>();
        }

    }
}
