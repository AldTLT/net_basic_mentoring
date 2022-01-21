using System;
using System.Data.Entity.ModelConfiguration;

namespace Gates.DAL.Entities
{
    public class CommentEntity
    {
        /// <summary>
        ///     Id комментария
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Содержимое комментария
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        ///     Дата создания комментария
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///     Login автора
        /// </summary>
        public string AuthorLogin { get; set; }

        /// <summary>
        ///     Навигационное свойство для автора
        /// </summary>
        public ApplicationUserEntity Author { get; set; }

        /// <summary>
        ///     Id задачи
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// Навигационное свойство для задачи
        /// </summary>
        //public TaskEntity Task { get; set; }

        /// <summary>
        ///     Конфигурация таблицы Comment
        /// </summary>
        public class CommentConfiguration : EntityTypeConfiguration<CommentEntity>
        {
            public CommentConfiguration()
            {
                ToTable("Comment");
                HasKey(c => c.Id);

                //HasRequired(c => c.Author)
                //    .WithMany(a => a.Comments)
                //    .HasForeignKey(c => c.AuthorLogin)
                //    .WillCascadeOnDelete(false);

                //HasRequired(c => c.Task)
                //    .WithMany(t => t.Comments)
                //    .HasForeignKey(c => c.TaskId)
                //    .WillCascadeOnDelete(false);

                Property(p => p.Content).IsRequired();
                Property(p => p.Content).HasMaxLength(400);
                Property(p => p.CreateDate).IsRequired();
            }
        }
    }
}