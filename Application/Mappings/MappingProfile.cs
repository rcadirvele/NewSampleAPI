using System;
using AutoMapper;
using NewSampleAPI.Domain.Model;
using NewSampleAPI.Application.Dtos;

namespace Application.Mappings
{
	public class MappingProfile : Profile
    {
		public MappingProfile()
		{
			CreateMap<CalcModel, CalcModelDto>().ReverseMap();
		}
	}
}

