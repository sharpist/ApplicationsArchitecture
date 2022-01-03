#nullable disable

namespace CleanArchitecture.Domain.Entities;

public class Employee : AuditableEntity
{
    public String Name       { get; set; }
    public String Department { get; set; }
}
