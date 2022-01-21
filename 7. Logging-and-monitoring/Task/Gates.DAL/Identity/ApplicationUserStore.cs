using Gates.DAL.DataAccess;
using Gates.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Gates.DAL.Identity
{
    /// <summary>
    ///     Промежуточный класс. Для обеспечения работы DI контейнера в менеджере пользователей
    /// </summary>
    public class ApplicationUserStore : UserStore<ApplicationUserEntity>, IUserStore
    {
        public ApplicationUserStore(UserContext dbContext)
            : base(dbContext)
        {
        }
    }
}