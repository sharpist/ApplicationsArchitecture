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

        CreateMap<Employee, UpdateEmployeeDTO>()
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
                dest => dest.Modified,
                opt => opt.MapFrom(src => src.Modified))
            .ReverseMap();

        CreateMap<Employee, DeleteEmployeeDTO>()
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
