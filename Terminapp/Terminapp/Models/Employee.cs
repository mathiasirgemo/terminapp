namespace Terminapp.Models;

public class Employee
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeEmail { get; set; }
    public decimal Employment { get; set; }
    public List<VacationRequest> VacationRequests { get; set; }
    
}