namespace CleanArchitecture.Core.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Employee, CreateEmployeeDTO>()
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(
                dest => dest.Department,
                opt => opt.MapFrom(src => src.Department))
            .ReverseMap();

        CreateMap<Employee, ReadEmployeeDTO>()
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(
                dest => dest.Department,
                opt => opt.MapFrom(src => src.Department))
            .ReverseMap();
    }
}
