namespace Identity.Domain.Mapping
{
    public class ApplicationRoleMapping : BaseMapping<ApplicationRole>
    {
        // Constructor to define entity mappings our model to database table.
        public ApplicationRoleMapping(EntityTypeBuilder<ApplicationRole> builder)
        {
            // Step 1: Map the 'Name' property to a Database column named "Name" with a max length of 255.
            builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(255);
        }
    }
}
