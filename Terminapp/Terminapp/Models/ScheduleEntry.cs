namespace Terminapp.Models;

public class ScheduleEntry
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string Shift { get; set; }
    public DateTime Date { get; set; }
}