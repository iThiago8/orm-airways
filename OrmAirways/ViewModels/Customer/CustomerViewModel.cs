namespace OrmAirways.ViewModels.Customer
{
    public class CustomerViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsVIP { get; set; }
    }
}
