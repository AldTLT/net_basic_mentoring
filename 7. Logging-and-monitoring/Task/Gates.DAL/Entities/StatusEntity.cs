using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Gates.DAL.Entities
{
    /// <summary>
    ///     Сущность Status
    /// </summary>
    public class StatusEntity
    {
        /// <summary>
        ///     Id статуса
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
        ///     Конфигурация таблицы Status
        /// </summary>
        public class StatusConfiguration : EntityTypeConfiguration<StatusEntity>
        {
            public StatusConfiguration()
            {
                ToTable("Status");

                HasKey(p => p.Id);
                Property(p => p.Title).IsRequired();
                Property(p => p.Id)
                    .IsRequired();
                Property(p => p.Title).HasMaxLength(20);
            }
        }
    }
}