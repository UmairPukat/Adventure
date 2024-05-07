namespace Adventure.Application.Interfaces
{
    public interface IPersonService
    {
        Task<List<PersonDto>> GetAll();
        Task<PersonDto> GetById(long id);
        Task<bool> Add(PersonDto model);
        Task<bool> Update(long Id, PersonDto model);
        Task<bool> Delete(Person model);
        Task<Person> FindById(long Id);
    }
}
