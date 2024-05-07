namespace Identity.Domain.Entities.Auth
{
    public class ApplicationUser : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AppId { get; set; }
        public string SecretKey { get; set; }
        public int RoleId { get; set; }
        public ApplicationRole ApplicationRole { get; set; }

    }
}
