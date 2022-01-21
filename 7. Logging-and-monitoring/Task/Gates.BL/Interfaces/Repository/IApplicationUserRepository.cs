using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Gates.BL.ViewModels;
using Microsoft.AspNet.Identity;

namespace Gates.BL.Interfaces.Repository
{
    /// <summary>
    ///     Репозиторий для работы с пользователями
    /// </summary>
    public interface IApplicationUserRepository
    {
        /// <summary>
        ///     Создание нового пользователя. С хешированием пароля.
        /// </summary>
        /// <param name="user">Новый пользователь</param>
        /// <param name="password">Пароль для хеширования</param>
        /// <returns></returns>
        Task<IdentityResult> CreateAsync(ApplicationUserViewModel user, string password);

        /// <summary>
        ///     Создает claims для определенного пользователя.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="authenticationType"></param>
        /// <returns></returns>
        Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUserViewModel user, string authenticationType);

        /// <summary>
        ///     Поиск пользователя по логину и паролю.
        /// </summary>
        /// <param name="userName">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns></returns>
        Task<ApplicationUserViewModel> FindAsync(string userName, string password);

        /// <summary>
        ///     Смена пароля пользователя
        /// </summary>
        /// <param name="userId">Ид пользователя</param>
        /// <param name="currentPassword">Действующий пароль пользователя</param>
        /// <param name="newPassword">Новый пароль</param>
        /// <returns></returns>
        Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);

        /// <summary>
        ///     Получить все существующие UserName
        /// </summary>
        /// <returns></returns>
        IQueryable<string> GetAllUserNames();
    }
}