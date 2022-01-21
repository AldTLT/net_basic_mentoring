using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Gates.BL.Interfaces.Repository;
using Gates.BL.ViewModels;
using Gates.DAL.Entities;
using Gates.DAL.Identity;
using Microsoft.AspNet.Identity;

namespace Gates.DAL.Repository
{
    // Настройка диспетчера пользователей приложения. UserManager определяется в ASP.NET Identity и используется приложением.

    /// <summary>
    ///     Менеджер пользователей приложения.
    /// </summary>
    public class ApplicationUserRepository : UserManager<ApplicationUserEntity>, IApplicationUserRepository
    {
        /// <summary>
        ///     Маппер сущностей
        /// </summary>
        private readonly IMapper _mapper;

        public ApplicationUserRepository(IUserStore userStore, IMapper mapper) : base(userStore)
        {
            _mapper = mapper;
            // Настройка логики проверки имен пользователей
            UserValidator = new UserValidator<ApplicationUserEntity>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false
            };

            // Настройка логики проверки паролей
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };
        }

        //Код мертвый. Но оставил в качестве напоминания. Т.к. не уверен для чего нужен dataProtectionProvider
        // public static ApplicationUserRepository Create(IdentityFactoryOptions<ApplicationUserRepository> options, IOwinContext context)
        // {
        //     var manager = new ApplicationUserRepository(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
        //
        //     var dataProtectionProvider = options.DataProtectionProvider;
        //     if (dataProtectionProvider != null)
        //     {
        //         manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
        //     }
        //     return manager;
        // }

        /// <summary>
        ///     Создание нового пользователя. С хешированием пароля.
        /// </summary>
        /// <param name="user">Новый пользователь</param>
        /// <param name="password">Пароль для хеширования</param>
        /// <returns></returns>
        public Task<IdentityResult> CreateAsync(ApplicationUserViewModel user, string password)
        {
            var userEntity = _mapper.Map<ApplicationUserEntity>(user);
            return base.CreateAsync(userEntity, password);
        }

        /// <summary>
        ///     Создает claims для определенного пользователя.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="authenticationType"></param>
        /// <returns></returns>
        public Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUserViewModel user, string authenticationType)
        {
            var userEntity = _mapper.Map<ApplicationUserEntity>(user);
            return base.CreateIdentityAsync(userEntity, authenticationType);
        }

        /// <summary>
        ///     Поиск пользователя по логину и паролю.
        /// </summary>
        /// <param name="userName">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns></returns>
        public new async Task<ApplicationUserViewModel> FindAsync(string userName, string password)
        {
            var userEntity = await base.FindAsync(userName, password);
            return _mapper.Map<ApplicationUserViewModel>(userEntity);
        }

        /// <summary>
        /// Получить список всех UserName
        /// </summary>
        /// <returns></returns>
        public IQueryable<string> GetAllUserNames()
        {
            return base.Users.Select(u => u.UserName);
        }
    }
}