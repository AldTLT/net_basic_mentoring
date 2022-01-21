using AutoMapper;
using Gates.BL.ViewModels;
using Gates.DAL.Entities;

namespace Gates.DAL.Mappers
{
    /// <summary>
    ///     Класс конфигуратор маппера. Содержит настройки маппинга
    /// </summary>
    public class EntityToViewModelProfile : Profile
    {
        /// <summary>
        ///     В конструкторе конфигурируем настроки маппинга
        /// </summary>
        public EntityToViewModelProfile()
        {
            //Настройка Comment раздел
            CreateMap<CommentEntity, CommentViewModel>();
            //.ForMember(member => member.Login, option => option.MapFrom(p => p.AuthorLogin));

            CreateMap<CommentViewModel, CommentEntity>();
            //.ForMember(member => member.AuthorLogin, option => option.MapFrom(p => p.Login));
            //.ForMember(member => member.Author, option => option.Ignore())
            //.ForMember(member => member.Task, option => option.Ignore());


            //Настройка Task раздел
            CreateMap<TaskEntity, TaskViewModel>();
            //.ForMember(member => member.Priority, option => option.MapFrom(p => p.Priority.Title))
            //.ForMember(member => member.Status, option => option.MapFrom(p => p.Status.Title))
            //.ForMember(member => member.Project, option => option.MapFrom(p => p.Title));

            CreateMap<TaskViewModel, TaskEntity>();
            //.ForMember(member => member.Comments, option => option.Ignore())
            //.ForMember(member => member.HashTags, option => option.Ignore())
            //.ForMember(member => member.Executor, option => option.Ignore())
            //.ForMember(member => member.Author, option => option.Ignore())
            //.ForMember(member => member.DeadLine, option => option.MapFrom(p => p.DeadlineDate))
            //.ForMember(member => member.Priority, option => option.Ignore())
            //.ForMember(member => member.Status, option => option.Ignore());

            CreateMap<ApplicationUserEntity, ApplicationUserViewModel>();
            CreateMap<ApplicationUserViewModel, ApplicationUserEntity>()
                .ForMember(member => member.Id, option => option.Condition(src => src.Id != null))
                .ForMember(member => member.Roles, option => option.Ignore())
                .ForMember(member => member.Claims, option => option.Ignore())
                .ForMember(member => member.Logins, option => option.Ignore());
        }
    }
}