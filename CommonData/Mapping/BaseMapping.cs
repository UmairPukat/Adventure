namespace CommonData.Mapping
{
    public abstract class BaseMapping<TEntity> : IEntityMap<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.CreatedBy).HasColumnName("CreatedBy");
            builder.Property(c => c.CreatedDate).HasColumnName("CreatedDate");
            builder.Property(c => c.UpdatedBy).HasColumnName("UpdatedBy");
            builder.Property(c => c.UpdatedDate).HasColumnName("UpdatedDate");
            builder.Property(c => c.IsActive).HasColumnName("IsActive").HasDefaultValue(true);
            builder.Property(c => c.IsDeleted).HasColumnName("IsDeleted").HasDefaultValue(false);
        }
    }
}
