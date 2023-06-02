namespace Common.DTO.Order
{
    public class UpdateOrderRequest
    {
        public int MemberId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequireDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public decimal Freight { get; set; }
    }
}
