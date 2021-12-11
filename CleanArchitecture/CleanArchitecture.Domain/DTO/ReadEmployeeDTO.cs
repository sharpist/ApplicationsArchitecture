#nullable disable

namespace CleanArchitecture.Domain.DTO;

public class ReadEmployeeDTO
{
    public Int32    EmployeeId { get; set; }
    public String   Name       { get; set; }
    public String   Department { get; set; }
    public DateTime AddedOn    { get; set; }
}
