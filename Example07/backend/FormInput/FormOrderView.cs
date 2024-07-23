using backend.Models;

namespace backend.FormInput
{
    public class FormOrderView
    {
        public string OrderNumber { get; set; }
        public long? UserId { get; set; }
        public long? ShippingId { get; set; }
        public decimal? Coupon { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public string Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public List<FormCartView> Carts { get; set; }
    }
}
