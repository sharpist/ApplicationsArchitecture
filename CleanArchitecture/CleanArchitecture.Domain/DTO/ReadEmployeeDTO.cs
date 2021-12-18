#nullable disable

namespace CleanArchitecture.Domain.DTO;

public class ReadEmployeeDTO : BaseEntity
{
    public String Name       { get; set; }
    public String Department { get; set; }
}
