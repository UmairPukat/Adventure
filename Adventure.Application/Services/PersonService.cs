namespace Adventure.Application.Services
{
    public class PersonService : IPersonService
    {
        // Step 1: Define private fields for database context and mapper.
        private readonly AdventureDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<PersonService> _logger;

        // Step 2: Constructor to initialize the service with required dependencies.
        public PersonService(AdventureDbContext dbContext
            , IMapper mapper
            , ILogger<PersonService> logger)
        {
            // Step 3: Assign the provided database context to the private field.
            _dbContext = dbContext;
            // Step 4: Assign the provided mapper to the private field.
            _mapper = mapper;
            // Step 5: Assign the provided log file to the private field.
            _logger = logger;

        }

        // Adds a new person record to the database.
        public async Task<bool> Add(PersonDto model)
        {
            // Step 1: Map DTO to Entity.
            Person pro = _mapper.Map<Person>(model);
            // Step 2: Set the creation date.
            pro.CreatedDate = DateTime.Now;

            // Step 3: Add the entity to the database.
            await _dbContext.Persons.AddAsync(pro);
            // Step 4: Save changes to the database.
            int res = await _dbContext.SaveChangesAsync();

            return res > 0 ? true : false;
        }

        // Deletes a person record from the database.
        public async Task<bool> Delete(Person model)
        {
            // Step 1: Remove the entity from the DbContext.
            _dbContext.Persons.Remove(model);
            // Step 2: Save changes to the database.
            int res = await _dbContext.SaveChangesAsync();

            return res > 0 ? true : false;
        }

        // Retrieves all person records from the database.
        public async Task<List<PersonDto>> GetAll()
        {
            // Step 1: Retrieve all entities from the DbContext and map them to DTOs.
            var result = await _dbContext.Persons
                .Select(x => new PersonDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Address = x.Address,
                    ContactNumber = x.ContactNumber
                }).ToListAsync();

            return result;
        }

        // Retrieves a specific person record by ID from the database.
        public async Task<PersonDto> GetById(long id)
        {
            PersonDto result = new PersonDto();

            // Step 1: Retrieve the entity with the given ID from the DbContext.
            var data = await _dbContext.Persons
                .Where(x => x.Id == id).FirstOrDefaultAsync();

            // Step 2: Map the retrieved entity to a DTO.
            if (data != null)
            {
                result = new PersonDto()
                {
                    Id = data.Id,
                    FirstName = data.FirstName,
                    MiddleName = data.MiddleName,
                    LastName = data.LastName,
                    Email = data.Email,
                    Address = data.Address,
                    ContactNumber = data.ContactNumber
                };
            }
            else
            {
                result = null;
            }

            return result;
        }

        // Updates a person record in the database.
        public async Task<bool> Update(long Id, PersonDto model)
        {
            // Step 1: Find the entity by ID.
            var result = await FindById(Id);
            if (result == null)
            {
                return false;
            }
            else
            {
                // Step 3: Set the updated data.
                result.FirstName = model.FirstName;
                result.MiddleName = model.MiddleName;
                result.LastName = model.LastName;
                result.Email = model.Email;
                result.Address = model.Address;
                result.ContactNumber = model.ContactNumber;
                result.UpdatedDate = DateTime.Now;

                // Step 4: Update the entity in the DbContext.
                _dbContext.Persons.Update(result);
                // Step 5: Save changes to the database.
                int res = await _dbContext.SaveChangesAsync();

                return res > 0 ? true : false;
            }
        }

        // Helper method to find a person by ID in the database.
        public async Task<Person> FindById(long Id)
        {
            // Step 1: Retrieve the entity with the given ID from the DbContext.
            var result = await _dbContext.Persons.FirstOrDefaultAsync(x => x.Id == Id);
            return result;
        }
    }

}

