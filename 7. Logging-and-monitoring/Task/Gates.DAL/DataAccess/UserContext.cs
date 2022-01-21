using System.Data.Entity;
using Gates.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Gates.DAL.DataAccess
{
    /// <summary>
    ///     Контекст подключения к БД
    /// </summary>
    // Строка подключения, использующя identity отключена.
    public class UserContext : IdentityDbContext<ApplicationUserEntity>
    {

        public UserContext()
            : base("DbConnection")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<UserContext>());
        }

        /// <summary>
        ///     Сущность Task для БД
        /// </summary>
        public DbSet<TaskEntity> Tasks { get; set; }

        /// <summary>
        ///     Сущность Project для БД
        /// </summary>
        public DbSet<ProjectEntity> Projects { get; set; }

        /// <summary>
        ///     Сущность Comment для БД
        /// </summary>
        public DbSet<CommentEntity> Comments { get; set; }

        /// <summary>
        ///     Сущность Status для БД
        /// </summary>
        public DbSet<StatusEntity> Status { get; set; }

        /// <summary>
        ///     Сущность Priority для БД
        /// </summary>
        public DbSet<PriorityEntity> Priority { get; set; }

        /// Сущность HashTag для БД
        /// </summary>
        public DbSet<HashTagEntity> HashTags { get; set; }

        /// <summary>
        ///     Сущность Role для БД
        /// </summary>
        public DbSet<RoleEntity> Roles { get; set; }

        /// <summary>
        ///     Fluent API
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Добавление конфигурации в модель.
            modelBuilder.Configurations.Add(new TaskEntity.TaskConfiguration());
            modelBuilder.Configurations.Add(new ProjectEntity.ProjectConfiguration());
            modelBuilder.Configurations.Add(new CommentEntity.CommentConfiguration());
            modelBuilder.Configurations.Add(new StatusEntity.StatusConfiguration());
            modelBuilder.Configurations.Add(new PriorityEntity.PriorityConfiguration());
            modelBuilder.Configurations.Add(new HashTagEntity.HashTagConfiguration());
            modelBuilder.Configurations.Add(new RoleEntity.RoleEntityConfiguration());

            //Создание модели.
            base.OnModelCreating(modelBuilder);
        }
    }
}
