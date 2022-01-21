using System.ComponentModel.DataAnnotations;

namespace Gates.BL.ViewModels
{
    /// <summary>
    ///     Данные поступающие при регистрации нового пользователя.
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        ///     Логин пользователя
        /// </summary>
        [Required]
        [StringLength(40, ErrorMessage = "Значение {0} должно содержать не {2} символов.", MinimumLength = 4)]
        public string UserName { get; set; }

        /// <summary>
        ///     Пароль пользователя
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}