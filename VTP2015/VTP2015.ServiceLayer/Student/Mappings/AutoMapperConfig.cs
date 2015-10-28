﻿using System;
using AutoMapper;
using VTP2015.Entities;

namespace VTP2015.ServiceLayer.Student.Mappings
{
    public class AutoMapperConfig 
    {
        public void Execute()
        {
            Mapper.CreateMap<Evidence, Models.Evidence>()
                .ForMember(r => r.StudentMail,
                    opt => opt.MapFrom(r => r.Student.Email));
            
            Mapper.CreateMap<File, Models.File>()
                .ForMember(r => r.Education,
                    opt => opt.MapFrom(r => r.Education.Name))
                .ForMember(r => r.StudentMail,
                    opt => opt.MapFrom(r => r.Student.Email));

            Mapper.CreateMap<PartimInformation, Models.PartimInformation>();

            Mapper.CreateMap<Request, Models.Request>()
                .ForMember(r => r.PartimInformationSuperCode,
                    opt => opt.MapFrom(r => r.PartimInformation.SuperCode))
                .ForMember(r => r.Status,
                    opt => opt.MapFrom(r => (Models.Status) r.Status));

            Mapper.CreateMap<Models.Evidence, Evidence>();
            Mapper.CreateMap<Models.File, File>();
            Mapper.CreateMap<Models.PartimInformation, PartimInformation>();
            Mapper.CreateMap<Models.Request, Request>();
        }

    }
}
