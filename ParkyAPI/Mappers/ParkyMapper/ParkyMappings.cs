using AutoMapper;
using ParkyAPI.Models;
using ParkyAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Mappers.ParkyMapper
{
    public class ParkyMappings : Profile
    {
        public ParkyMappings()
        {
            CreateMap<NationalParkModel, NationalParkDto>().ReverseMap();
            CreateMap<NationalParkModel, NationalParkCreateDto>().ReverseMap();
            CreateMap<TrailModel, TrailDto>().ReverseMap();
            CreateMap<TrailModel, TrailCreateDto>().ReverseMap();
            CreateMap<TrailModel, TrailUpdateDto>().ReverseMap();
        }
    }
}
