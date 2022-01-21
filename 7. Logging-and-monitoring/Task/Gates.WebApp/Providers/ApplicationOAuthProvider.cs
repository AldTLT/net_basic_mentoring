using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Gates.BL.Interfaces.WebApi;
using Gates.BL.ViewModels;
using Gates.DAL.DataAccess;
using Gates.WebApp.Infrastructure;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using NLog;

namespace Gates.WebApp.Providers
{
    /// <summary>
    ///     Провайдер авторизации. Используется для связи с компонентами OWIN.
    /// </summary>
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly IApplicationUserManager _manager;
        private readonly string _publicClientId;
        private readonly ILogger _logger;

        public ApplicationOAuthProvider(IApplicationUserManager manager, string publicClientId)
        {
            _publicClientId = publicClientId ?? throw new ArgumentNullException(nameof(publicClientId));
            _manager = manager;
            _logger = LogManager.GetCurrentClassLogger();
        }

        //Метод вызываемый для проверки учетных данных при запросе токена.
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            _logger.Info($"Authorization. User: [{context.UserName}]");
            //Поиск учетных данных
            var userDto = await _manager.FindAsync(context.UserName, context.Password);
            //Если пользователь не найден
            if (userDto == null)
            {
                _logger.Error($"Authorization failed. User: [{context.UserName}]");
                context.SetError("invalid_grant", "Имя пользователя или пароль указаны неправильно.");
                return;
            }

            var oAuthIdentity = await GenerateUserIdentityAsync(_manager, userDto, OAuthDefaults.AuthenticationType);
            // ClaimsIdentity cookiesIdentity = await GenerateUserIdentityAsync(_manager,
            //     CookieAuthenticationDefaults.AuthenticationType);

            var properties = CreateProperties(userDto.UserName);
            var ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);

            _logger.Info($"Authorization successfull. User: [{context.UserName}]");

            // Increase counter.
            Counter.Counters.Increment(Counters.Authorization);
        }

        /// <summary>
        ///     Метод генерирует claims. Которые необходимо добавить в токен.
        ///     Claims - некоторые объекты, которые можно использовать при авторизации.
        /// </summary>
        /// <param name="manager">Менеджер пользователей</param>
        /// <param name="userViewModel">Пользователь для которого генерируются Claims</param>
        /// <param name="authenticationType">Тип аутентификации</param>
        /// <returns></returns>
        private async Task<ClaimsIdentity> GenerateUserIdentityAsync(IApplicationUserManager manager,
            ApplicationUserViewModel userViewModel, string authenticationType)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(userViewModel, authenticationType);
            // Здесь добавьте настраиваемые утверждения пользователя
            return userIdentity;
        }

        /// <summary>
        ///     Метод добавляет дополнительные данные в токен.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var property in context.Properties.Dictionary)
                context.AdditionalResponseParameters.Add(property.Key, property.Value);

            return Task.FromResult<object>(null);
        }

        /// <summary>
        ///     Проверяет cliend_id источника на регистрацию.
        ///     Используется при авторизации через Google,Vk...
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Учетные данные владельца ресурса не содержат идентификатор клиента.
            if (context.ClientId == null) context.Validated();

            return Task.FromResult<object>(null);
        }

        /// <summary>
        ///     Производиться тривиальная проверка clientId.
        ///     Вызов этого метода обязателен, для успешного продолжения запроса.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                var expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri) context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        /// <summary>
        ///     Генерирует утверждения, которые будут добавлены в токен.
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <returns></returns>
        private static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                {"userName", userName}
            };
            return new AuthenticationProperties(data);
        }
    }
}