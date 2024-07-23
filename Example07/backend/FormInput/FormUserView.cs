namespace backend.FormInput
{
    public class FormUserView
    {
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Photo { get; set; }
        public string Role { get; set; } = null!;
        public string? Provider { get; set; }
        public string? ProviderId { get; set; }
        public string Status { get; set; } = null!;

    }
}
