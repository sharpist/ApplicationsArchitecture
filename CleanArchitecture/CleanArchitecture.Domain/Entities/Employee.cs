#nullable disable

namespace CleanArchitecture.Domain.Entities;

[Table("Employee")]
public class Employee : BaseEntity
{
    public String Name       { get; set; }
    public String Department { get; set; }
}
