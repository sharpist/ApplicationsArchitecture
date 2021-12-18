﻿namespace CleanArchitecture.Core.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Employee, CreateOrUpdateEmployeeDTO>()
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(
                dest => dest.Department,
                opt => opt.MapFrom(src => src.Department))
            .ReverseMap();

        CreateMap<Employee, ReadEmployeeDTO>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(
                dest => dest.Department,
                opt => opt.MapFrom(src => src.Department))
            .ForMember(
                dest => dest.Created,
                opt => opt.MapFrom(src => src.Created))
            .ReverseMap();
    }
}
