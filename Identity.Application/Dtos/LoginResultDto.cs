namespace Identity.Application.Dtos
{
    public class LoginResultDto
    {
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
        public int RoleId { get; set; }
    }
}
