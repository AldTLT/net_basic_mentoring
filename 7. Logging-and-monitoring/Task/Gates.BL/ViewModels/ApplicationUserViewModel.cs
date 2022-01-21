using System;
using Microsoft.AspNet.Identity;

namespace Gates.BL.ViewModels
{
    /// <summary>
    ///     ViewModel профиля пользователя.
    /// </summary>

    //IUser - необходим для некоторых метод используемых в Identity
    public class ApplicationUserViewModel : IUser<string>
    {
        /// <summary>
        ///     Почта пользователя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Идентификатор показывающий подтверждена ли почта пользователя
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        ///     Хеш пароля
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        ///     Отметка безопасности
        /// </summary>
        public string SecurityStamp { get; set; }

        /// <summary>
        ///     Телефон пользователя
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     Идентификатор показывающий подтвержден ли телефон пользователя
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        ///     Идентификатор показывающий включена ли двухфакторная авторизации
        /// </summary>
        public bool TwoFactorEnabled { get; set; }

        /// <summary>
        ///     Дата окончания блокировки аккаунта
        /// </summary>
        public DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>
        ///     Активна ли блокировка аккаунта
        /// </summary>
        public bool LockoutEnabled { get; set; }

        /// <summary>
        ///     Количество неудачных попыток входа
        /// </summary>
        public int AccessFailedCount { get; set; }

        //Расскоментировать при необходимости доступа к полям
        //public  ICollection<IdentityUserRole> Roles { get; set; }
        //public  ICollection<IdentityUserClaim> Claims { get; set; }
        //public  ICollection<IdentityUserLogin> Logins { get; set; }
        /// <summary>
        ///     Id пользователя
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Логин пользователя
        /// </summary>
        public string UserName { get; set; }
    }
}