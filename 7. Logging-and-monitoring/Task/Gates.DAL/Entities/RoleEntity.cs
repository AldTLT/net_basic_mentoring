using System.Data.Entity.ModelConfiguration;

namespace Gates.DAL.Entities
{
    /// <summary>
    /// Сущность Role
    /// </summary>
    public class RoleEntity
    {
        /// <summary>
        /// Id Роли
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название роли
        /// </summary>
        public string Title { get; set; }

        public class RoleEntityConfiguration : EntityTypeConfiguration<RoleEntity>
        {
            public RoleEntityConfiguration()
            {
                ToTable("Role");

                HasKey(p => p.Id);

                Property(p => p.Title).IsRequired();
                Property(p => p.Title).HasMaxLength(40);
            }
        }
    }
}
