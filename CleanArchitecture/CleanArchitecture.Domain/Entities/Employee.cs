#nullable disable

namespace CleanArchitecture.Domain.Entities;

public class Employee : BaseEntity
{
    public String Name       { get; set; }
    public String Department { get; set; }
}
