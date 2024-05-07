namespace Adventure.Domain.Mapping
{
    public class PersonMapping : BaseMapping<Person>
    {
        // Constructor to define entity mappings our model to database table.
        public PersonMapping(EntityTypeBuilder<Person> builder)
        {
            // Step 1: Map the 'FirstName' property to a Database column named "FirstName" with a max length of 255.
            builder.Property(x => x.FirstName).HasColumnName("FirstName").HasMaxLength(255);

            // Step 2: Map the 'MiddleName' property to a Database column named "MiddleName" with a max length of 255.
            builder.Property(x => x.MiddleName).HasColumnName("MiddleName").HasMaxLength(255);

            // Step 3: Map the 'LastName' property to a Database column named "LastName" with a max length of 100.
            builder.Property(x => x.LastName).HasColumnName("LastName").HasMaxLength(100);

            // Step 4: Map the 'Email' property to a Database column named "Email" with a max length of 255.
            builder.Property(x => x.Email).HasColumnName("Email").HasMaxLength(255);

            // Step 5: Map the 'Address' property to a Database column named "Address" with a max length of 100.
            builder.Property(x => x.Address).HasColumnName("Address").HasMaxLength(100);

            // Step 6: Map the 'ContactNumber' property to a Database column named "ContactNumber" with a max length of 255.
            builder.Property(x => x.ContactNumber).HasColumnName("ContactNumber").HasMaxLength(255);
        }
    }

}
