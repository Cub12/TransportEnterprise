namespace DAL;

public class Maintenance
{
    public int id { get; set; }
    public DateTime date { get; set; }
    public string serviceType { get; set; }
    public string description { get; set; }
    public IEnumerable<Vehicle> vehicles { get; set; }
}