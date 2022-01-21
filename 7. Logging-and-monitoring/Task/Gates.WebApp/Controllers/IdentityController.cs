using System.Threading.Tasks;
using System.Web.Http;
using Gates.BL.Interfaces.WebApi;
using Gates.BL.ViewModels;
using Gates.WebApp.Infrastructure;
using Microsoft.AspNet.Identity;

namespace Gates.WebApp.Controllers
{
    /// <summary>
    ///     Реализует взаимодействие с Identity.
    ///     Регистрация, измение пароли и т.д.
    /// </summary>
    [Authorize]
    public class IdentityController : ApiController
    {
        /// <summary>
        ///     Менеджер пользователей
        /// </summary>
        private readonly IApplicationUserManager _userManager;

        public IdentityController(IApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        ///     Регистрация нового пользователя
        /// </summary>
        /// <param name="model">Данные для регистрации</param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IHttpActionResult> Register(RegisterViewModel model)
        {
            if (model == null) return BadRequest("Не указаны данные.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new ApplicationUserViewModel {UserName = model.UserName};

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Increase counter.
                Counter.Counters.Increment(Counters.Authentication);

                return Ok();
            }

            return GetErrorResult(result);
        }

        /// <summary>
        ///     Изменить пароль пользователя
        /// </summary>
        /// <param name="model">Данные для смены пароля</param>
        /// <returns></returns>
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (model == null) return BadRequest("Не указаны данные.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _userManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                model.NewPassword);

            return !result.Succeeded ? GetErrorResult(result) : Ok();
        }

        /// <summary>
        ///     Получить все UserName
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetAllUserNames()
        {
            return Ok(_userManager.GetAllUserNames());
        }

        #region Вспомогательные приложения

        /// <summary>
        ///     Добавляет ошибки в ModelState для отправки клиенту.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null) return InternalServerError();

            if (result.Succeeded) return null;
            if (result.Errors != null)
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error);

            if (ModelState.IsValid)
                // Ошибки ModelState для отправки отсутствуют, поэтому просто возвращается пустой BadRequest.
                return BadRequest();

            return BadRequest(ModelState);
        }

        #endregion
    }
}