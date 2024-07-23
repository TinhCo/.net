namespace backend.FormInput
{
    public class FormNotificationView
    {
        public string Id { get; set; }
        public string Type { get; set; } = null!;
        public string NotifiableType { get; set; } = null!;
        public long NotifiableId { get; set; }
        public string Data { get; set; } = null!;
    }
}
