namespace backend.FormInput
{
    public class FormCouponView
    {
        public string Code { get; set; } = null!;
        public string Type { get; set; } = null!;
        public decimal Value { get; set; }
        public string Status { get; set; } = null!;
    }
}
