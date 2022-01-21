using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Gates.DAL.Entities
{
    /// <summary>
    ///     Сущность HashTag
    /// </summary>
    public class HashTagEntity
    {
        /// <summary>
        ///     Id хеш-тега
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Заголовок хеш-тега
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        ///     Навигационное свойство для задач
        /// </summary>
        public ICollection<TaskEntity> Tasks { get; set; }

        /// <summary>
        ///     Конфигурация таблицы HashTag
        /// </summary>
        public class HashTagConfiguration : EntityTypeConfiguration<HashTagEntity>
        {
            public HashTagConfiguration()
            {
                ToTable("HashTag");

                HasKey(p => p.Id);

                Property(p => p.Title).IsRequired();
                Property(p => p.Title).HasMaxLength(40);
            }
        }
    }
}