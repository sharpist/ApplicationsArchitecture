namespace CleanArchitecture.Domain.Entities;

public abstract class AuditableEntity : BaseEntity
{
    public virtual DateTime? Created  { get; set; }
    public virtual DateTime? Modified { get; set; }
}
