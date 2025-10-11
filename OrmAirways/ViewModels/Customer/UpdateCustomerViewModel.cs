using System.ComponentModel.DataAnnotations;

namespace OrmAirways.ViewModels.Customer
{
    public class UpdateCustomerViewModel
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string CPF { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsVIP { get; set; }

    }
}
