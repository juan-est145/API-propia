namespace API_propia.Data_Models
{
    public class UserTokens
    {
        public int Id { get; set; } //Added into JwtHelpers
        public string Token { get; set; } //Added into JwTHelpers
        public string UserName { get; set; } //Added into JwtHelpers
        public TimeSpan Validity { get; set; } //Added into JWtHelpers
        public string RefreshToken { get; set; }
        public string EmailId { get; set; } //Added into JwtHelpers
        public string Role { get; set; } //Added into JwtHelpers
        public Guid GuiId { get; set; } //Added into JwtHelpers
        public DateTime ExpiredTime { get; set; } //Added into JwtHelpers
    }
}
