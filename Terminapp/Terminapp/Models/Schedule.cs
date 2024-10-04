namespace Terminapp.Models;

public class Schedule
{
    public int ScheduleId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Comments { get; set; }
    public string Status { get; set; }
    public List<ScheduleEntry> Entries { get; set; }
    
    public List<VacationRequest> VacationRequests { get; set; }
}