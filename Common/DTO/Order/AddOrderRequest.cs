namespace Common.DTO.Order
{
    public class AddOrderRequest
    {
        public int MemberId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequireDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public decimal Freight { get; set; }
        public List<int> ProductId { get; set; }
        public int Quantity { get; set; }
        public double? Discount { get; set; }
    }
}
