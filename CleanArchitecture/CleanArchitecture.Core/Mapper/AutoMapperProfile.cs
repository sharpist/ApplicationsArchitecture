namespace CleanArchitecture.Core.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Employee, PostEmployeeCommand>()
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(
                dest => dest.Department,
                opt => opt.MapFrom(src => src.Department))
            .ReverseMap();

        CreateMap<Employee, PutEmployeeCommand>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(
                dest => dest.Department,
                opt => opt.MapFrom(src => src.Department))
            .ReverseMap();
    }
}
