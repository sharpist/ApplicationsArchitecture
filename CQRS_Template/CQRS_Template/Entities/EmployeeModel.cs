#nullable disable

namespace CQRS_Template.Entities;

[Table("Employee")]
public class EmployeeModel
{
    [JsonIgnore]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Int32  EmployeeId { get; set; }
    public String Name       { get; set; }
    public String Department { get; set; }
}
