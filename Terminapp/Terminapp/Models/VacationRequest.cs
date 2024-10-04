namespace Terminapp.Models;

public class VacationRequest
{
    public int VacationId { get; set; }
    public int EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; }
    public string Comment { get; set; }
    
    public Employee Employee { get; set; }

}