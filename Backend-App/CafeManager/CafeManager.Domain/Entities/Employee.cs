namespace CafeManager.Domain.Entities;

public class Employee
{
    public string Id { get; set; } = string.Empty; // format: UIXXXXXXX
    public string Name { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }

    public Guid? CafeId { get; set; }
    public Cafe? Cafe { get; set; }
}
