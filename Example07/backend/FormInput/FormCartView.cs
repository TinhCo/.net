namespace backend.FormInput
{
    public class FormCartView
    {
        public long ProductId { get; set; }
        //public long? UserId { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; } = null!;
        public int Quantity { get; set; }

    }
}
