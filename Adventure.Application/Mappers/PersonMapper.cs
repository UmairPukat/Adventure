namespace Adventure.Application.Mappers
{
    public class PersonMapper : Profile
    {
        // Step 1: Constructor for PersonMapper to define mappings.
        public PersonMapper()
        {
            // Step 2: Define a mapping from Person entity to PersonDto.
            CreateMap<Person, PersonDto>();

            // Step 3: Define a mapping from PersonDto to Person entity.
            CreateMap<PersonDto, Person>();
        }
    }

}