namespace Identity.Application.Dtos
{
    public class RegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AppId { get; set; }
        public string SecretKey { get; set; }
        public int RoleId { get; set; }
    }
}
