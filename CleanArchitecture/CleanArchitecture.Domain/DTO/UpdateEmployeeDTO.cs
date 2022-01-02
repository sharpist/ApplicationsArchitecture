#nullable disable

namespace CleanArchitecture.Domain.DTO;

public class UpdateEmployeeDTO : BaseEntity
{
    public String Name       { get; set; }
    public String Department { get; set; }
}
