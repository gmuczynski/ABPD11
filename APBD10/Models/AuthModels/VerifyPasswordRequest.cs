namespace APBD10.Models.AuthModels
{
    public class VerifyPasswordRequest
    {
        public string Password { get; set; } = null!;
        public string Hash { get; set; } = null!;
    }
}
