using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Gates.BL.Interfaces.Repository;
using Gates.BL.Interfaces.WebApi;
using Gates.BL.ViewModels;
using Microsoft.AspNet.Identity;
using NLog;

namespace Gates.BL.Managers
{
    /// <summary>
    ///     Менеджер для работы с пользователями
    /// </summary>
    public class ApplicationUserManager : IApplicationUserManager
    {
        private readonly IApplicationUserRepository _repository;
        private readonly ILogger _logger;

        public ApplicationUserManager(IApplicationUserRepository repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        ///     Создание нового пользователя. С хешированием пароля.
        /// </summary>
        /// <param name="user">Новый пользователь</param>
        /// <param name="password">Пароль для хеширования</param>
        /// <returns></returns>
        public Task<IdentityResult> CreateAsync(ApplicationUserViewModel user, string password)
        {
            _logger.Info($"Create new user. User: [{user.UserName}]");

            return _repository.CreateAsync(user, password);
        }

        /// <summary>
        ///     Создает claims для определенного пользователя.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="authenticationType"></param>
        /// <returns></returns>
        public Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUserViewModel user, string authenticationType)
        {
            _logger.Info($"Create claims. User: [{user.UserName}].");

            return _repository.CreateIdentityAsync(user, authenticationType);
        }

        /// <summary>
        ///     Поиск пользователя по логину и паролю.
        /// </summary>
        /// <param name="userName">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns></returns>
        public Task<ApplicationUserViewModel> FindAsync(string userName, string password)
        {
            _logger.Info($"Find user. User: [{userName}].");

            return _repository.FindAsync(userName, password);
        }

        /// <summary>
        ///     Смена пароля пользователя
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <param name="currentPassword">Действующий пароль пользователя</param>
        /// <param name="newPassword">Новый пароль</param>
        /// <returns></returns>
        public Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            _logger.Info($"Change password. User: [{userId}].");

            return _repository.ChangePasswordAsync(userId, currentPassword, newPassword);
        }

        /// <summary>
        /// Получить список всех UserName
        /// </summary>
        /// <returns></returns>
        public IQueryable<string> GetAllUserNames()
        {
            _logger.Info($"Get all usernames.");

            return _repository.GetAllUserNames();
        }
    }
}