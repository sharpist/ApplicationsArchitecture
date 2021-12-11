#nullable disable

namespace CleanArchitecture.Domain.Entities;

public abstract class BaseEntity
{
    [JsonIgnore]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual Int32    Id      { get; set; }
    public virtual DateTime AddedOn { get; set; }
}
