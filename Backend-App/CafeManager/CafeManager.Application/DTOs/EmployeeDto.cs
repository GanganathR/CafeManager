namespace CafeManager.Application.DTOs;

public class EmployeeDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public int DaysWorked { get; set; }

    public Guid? CafeId { get; set; }       
    public string CafeName { get; set; } = string.Empty; 
}
