namespace OrmAirways.Models
{
    public class Airport : BaseEntity
    {
        public string IataCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public Address Location { get; set; } = new();
    }
}
