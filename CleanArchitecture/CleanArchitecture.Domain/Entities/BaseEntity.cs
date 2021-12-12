#nullable disable

namespace CleanArchitecture.Domain.Entities;

public abstract class BaseEntity
{
    public virtual Int32    Id      { get; set; }
    public virtual DateTime AddedOn { get; set; }
}
