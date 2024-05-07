namespace Identity.Domain.Mapping
{
    public class ApplicationUserMapping : BaseMapping<ApplicationUser>
    {
        // Constructor to define entity mappings our model to database table.
        public ApplicationUserMapping(EntityTypeBuilder<ApplicationUser> builder)
        {
            // Step 1: Map the 'FirstName' property to a Database column named "FirstName" with a max length of 255.
            builder.Property(x => x.FirstName).HasColumnName("FirstName").HasMaxLength(255);

            // Step 3: Map the 'LastName' property to a Database column named "LastName" with a max length of 100.
            builder.Property(x => x.LastName).HasColumnName("LastName").HasMaxLength(100);

            // Step 4: Map the 'AppId' property to a Database column named "AppId" with a max length.
            builder.Property(x => x.AppId).HasColumnName("AppId");

            // Step 5: Map the 'SecretKey' property to a Database column named "SecretKey" with a max lengt.
            builder.Property(x => x.SecretKey).HasColumnName("SecretKey");

            // Step 6: Create Relationship between 'ApplicationUser' and 'ApplicationRole'
            builder.HasOne(x => x.ApplicationRole).WithMany(x => x.ApplicationUsers).HasForeignKey(x => x.RoleId);
        }
    }
}

