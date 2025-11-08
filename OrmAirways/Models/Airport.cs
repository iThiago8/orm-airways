using System.ComponentModel.DataAnnotations;

namespace OrmAirways.Models
{
    public class Airport
    {
      [Key]
      public int ID { get; set; }
      public string Name { get; set; } = string.Empty;
      public string Street { get; set; } = string.Empty;
      public string City { get; set; } = string.Empty;
      public string District { get; set; } = string.Empty;
      public string State { get; set; } = string.Empty;
      public string Country { get; set; } = string.Empty;
      public string PostalCode { get; set; } = string.Empty;
    }
}
