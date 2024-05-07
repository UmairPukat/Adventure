namespace Adventure.Infrastructure.Data
{
    public class AdventureDbContext : DbContext
    {
        // Constructor to initialize the DbContext with provided options.
        public AdventureDbContext(DbContextOptions<AdventureDbContext> options) : base(options)
        {
        }

        // Step 1: Override the OnModelCreating method to configure the model.
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Step 2: Call the base implementation for any default model configuration.
            base.OnModelCreating(builder);

            // Step 3: Apply the entity mapping configuration for the model entity.
            new PersonMapping(builder.Entity<Person>());
           // new ApplicationRoleMapping(builder.Entity<ApplicationRole>());
        }

        // Step 4: Define a DbSet for the Model entity to interact with the corresponding database table.
        public DbSet<Person> Persons { get; set; }
     //   public DbSet<ApplicationRole> ApplicationRoles { get; set; }
    }
}
