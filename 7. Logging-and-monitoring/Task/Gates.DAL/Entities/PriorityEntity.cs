using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Gates.DAL.Entities
{
    /// <summary>
    ///     Сущность Priority
    /// </summary>
    public class PriorityEntity
    {
        /// <summary>
        ///     Id приоритета
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Название
        /// </summary>
        public string Title { get; set; }

        ///// <summary>
        ///// Навигационное свойство 
        ///// </summary>
        public ICollection<TaskEntity> Task { get; set; }


        /// <summary>
        ///     Конфигурация таблицы Priority
        /// </summary>
        public class PriorityConfiguration : EntityTypeConfiguration<PriorityEntity>
        {
            public PriorityConfiguration()
            {
                ToTable("Priority");

                HasKey(p => p.Id);
                Property(p => p.Title).IsRequired();
                Property(p => p.Id)
                    .IsRequired();
                Property(p => p.Title).HasMaxLength(20);
            }
        }
    }
}