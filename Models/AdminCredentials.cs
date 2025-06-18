namespace HareKrishnaNamaHattaWebApp.Models
{
    public class AdminCredentials
    {
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? ErrorMessage { get; set; }
    }
}
