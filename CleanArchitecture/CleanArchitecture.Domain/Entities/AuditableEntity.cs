namespace CleanArchitecture.Domain.Entities;

public abstract class AuditableEntity : BaseEntity
{
    public virtual DateTime? Modified { get; set; }
}
