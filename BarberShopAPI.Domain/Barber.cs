using BarberShopAPI.Domain;

public class Barber
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Password { get; set; }
    public ICollection<Appointment>? Appointments { get; set; }
    public TimeSpan? LunchStartTime { get; set; } 
    public TimeSpan? LunchEndTime { get; set; }

}
