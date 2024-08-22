namespace rhinobill.sql.Entities.Configuration;

public class CourseEntityConfiguration : IEntityTypeConfiguration<CourseEntity>
{
    void IEntityTypeConfiguration<CourseEntity>.Configure(EntityTypeBuilder<CourseEntity> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
