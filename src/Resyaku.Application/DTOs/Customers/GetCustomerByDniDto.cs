namespace Resyaku.Application.DTOs.Customers
{
    public class GetCustomerByDniDto
    {
        public string CustomerName { get; set; } = null!;
        public string? CustomerLastname { get; set; }
        public string CustomerDni { get; set; } = null!;
        public string CustomerEmail { get; set; } = null!;
        public string CustomerPhone { get; set; } = null!;
    }
}
