namespace rhinobill.sql.Entities.Configuration;

public class StudentEntityConfiguration : IEntityTypeConfiguration<StudentEntity>
{
    void IEntityTypeConfiguration<StudentEntity>.Configure(EntityTypeBuilder<StudentEntity> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
