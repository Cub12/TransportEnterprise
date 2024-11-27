namespace DAL.Entities;

public class Driver
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? CardNumber { get; set; }
    public DateTime BirthDate { get; set; }
}