namespace DAL;

public class Route
{
    public int id { get; set; }
    public string startingPoint { get; set; }
    public string endPoint { get; set; }
    public float length { get; set; }
    public IEnumerable<Driver> drivers { get; set; }
    public IEnumerable<Vehicle> vehicles { get; set; }
}