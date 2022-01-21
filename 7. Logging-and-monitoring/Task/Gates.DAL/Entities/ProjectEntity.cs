using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Gates.DAL.Entities
{
    public class ProjectEntity
    {
        public ProjectEntity()
        {
            Tasks = new List<TaskEntity>();
        }

        /// <summary>
        /// Id проекта
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование проекта
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Login автора
        /// </summary>
        public string AuthorId { get; set; }

        /// <summary>
        /// Проекты обладают несколькими тасками
        /// </summary>
        public ICollection<TaskEntity> Tasks { get; set; }


        /// <summary>
        /// Навигационное свойство
        /// </summary>
        public ApplicationUserEntity Author { get; set; }

        /// <summary>
        /// Дата создания проекта
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Конфигурация таблицы Project
        /// </summary>
        public class ProjectConfiguration : EntityTypeConfiguration<ProjectEntity>
        {
            public ProjectConfiguration()
            {
                ToTable("Project");
                HasKey(p => p.Id);

                HasRequired(c => c.Author)
                    .WithMany(a => a.Projects)
                    .HasForeignKey(c => c.AuthorId)
                    .WillCascadeOnDelete(false);

                //Код отключен как нерабочий. Может быть включен потом.
                //Отношение многие ко многим
                //HasMany(t => t.Tasks)
                //    .WithMany(h => h.Projects)
                //    .Map(th =>
                //    {
                //        th.ToTable("ProjectTaskRelationship");
                //        th.MapLeftKey("ProjectId");
                //        th.MapRightKey("TaskId");

                //    });

                Property(p => p.Title).IsRequired();
                Property(p => p.Title).HasMaxLength(40);
                Property(p => p.CreateDate).IsRequired();
            }
        }
    }
}