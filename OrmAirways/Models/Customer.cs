using System.ComponentModel.DataAnnotations;

namespace OrmAirways.Models
{
    public class Customer
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsVIP { get; set; }
    }
}
