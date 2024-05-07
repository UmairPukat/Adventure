namespace CommonData.Interfaces
{
    public interface IEntityMap<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {

    }
}
