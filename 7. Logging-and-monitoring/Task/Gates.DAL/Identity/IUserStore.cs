using Gates.DAL.Entities;
using Microsoft.AspNet.Identity;

namespace Gates.DAL.Identity
{
    /// <summary>
    ///     Промежуточный интерфейс. Используется для обеспечения работы DI контейнера
    /// </summary>
    public interface IUserStore : IUserStore<ApplicationUserEntity>
    {
    }
}