#nullable disable

namespace CleanArchitecture.Domain.DTO;

public class UpdateEmployeeDTO : AuditableEntity
{
    public String Name       { get; set; }
    public String Department { get; set; }
}
