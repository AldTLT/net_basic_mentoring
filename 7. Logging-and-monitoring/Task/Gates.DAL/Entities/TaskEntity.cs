using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Gates.DAL.Entities
{
    /// <summary>
    ///     Сущность Task
    /// </summary>
    public class TaskEntity
    {
        /// <summary>
        ///     Id Задачи
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Название
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Описание задачи
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Дедлайн задачи
        /// </summary>
        public DateTime DeadlineDate { get; set; }

        /// <summary>
        ///     Id приоритета.
        /// </summary>
        public int PriorityId { get; set; }

        /// <summary>
        ///     Приоритет задачи
        /// </summary>
        public PriorityEntity Priority { get; set; }

        /// <summary>
        ///     Дата создания задачи
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///     Id статуса.
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        ///     Статус задачи
        /// </summary>
        public StatusEntity Status { get; set; }

        /// <summary>
        ///     Login автора
        /// </summary>
        public string AuthorId { get; set; }

        /// <summary>
        ///     Навигационное свойство для автора
        /// </summary>
        public ApplicationUserEntity Author { get; set; }

        /// <summary>
        ///     Login исполнителя
        /// </summary>
        public string ExecutorId { get; set; }

        /// <summary>
        ///     Навигационное свойство для исполнителя
        /// </summary>
        public ApplicationUserEntity Executor { get; set; }

        /// <summary>
        ///     Id Проекта
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        ///     Навигационное свойство.
        /// </summary>
        public ProjectEntity Project { get; set; }

        /// <summary>
        ///     Такси обладают несколькими хештегами
        /// </summary>
        public ICollection<HashTagEntity> HashTags { get; set; }

        /// <summary>
        ///     Навигационное свойство для проектов
        /// </summary>
        //public ICollection<ProjectEntity> Projects { get; set; }

        /// <summary>
        ///     Таски обладают несколькими комментариями
        /// </summary>
        //public ICollection<CommentEntity> Comments;

        public TaskEntity()
        {
            //Projects = new List<ProjectEntity>();
        }


        /// <summary>
        ///     Конфигурация таблицы Task
        /// </summary>
        public class TaskConfiguration : EntityTypeConfiguration<TaskEntity>
        {
            public TaskConfiguration()
            {
                ToTable("Task");
                HasKey(t => t.Id);

                HasRequired(a => a.Author)
                    .WithMany(t => t.TasksAuthor)
                    .HasForeignKey(a => a.AuthorId)
                    .WillCascadeOnDelete(false);

                HasRequired(a => a.Executor)
                    .WithMany(a => a.TasksExecutor)
                    .HasForeignKey(a => a.ExecutorId)
                    .WillCascadeOnDelete(false);

                Property(p => p.DeadlineDate)
                    .HasColumnName("DeadLine");

                Property(a => a.AuthorId)
                    .IsRequired();

                Property(a => a.ExecutorId)
                    .IsRequired();

                Property(a => a.ProjectId)
                    .IsOptional();

                HasOptional(p => p.Project);

                HasRequired(a => a.Executor)
                    .WithMany(t => t.TasksExecutor)
                    .HasForeignKey(a => a.ExecutorId)
                    .WillCascadeOnDelete(false);

                //Отношение многие ко многим
                HasMany(t => t.HashTags)
                    .WithMany(h => h.Tasks)
                    .Map(th =>
                    {
                        th.MapLeftKey("TaskID");
                        th.MapRightKey("HashTagID");
                        th.ToTable("TaskHashRelationship");
                    });

                HasRequired(p => p.Priority);

                Property(p => p.Description).IsRequired();
                Property(p => p.Description).HasMaxLength(200);
                Property(p => p.DeadlineDate).IsRequired();
                Property(p => p.CreateDate).IsRequired();
            }
        }
    }
}