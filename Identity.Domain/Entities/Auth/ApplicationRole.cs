namespace Identity.Domain.Entities.Auth
{
    public class ApplicationRole : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
