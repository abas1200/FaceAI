using FaceAI.Application.Authorization.Option;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace FaceAI.Application.Authorization
{

    public class AuthHeaderHandler<TOption> : DelegatingHandler
    where TOption : class, ITokenEndPointOption
    {
        private readonly TOption _option;
        private readonly ILogger<AuthHeaderHandler<TOption>> _logger;

        public AuthHeaderHandler(IOptions<TOption> provider, ILogger<AuthHeaderHandler<TOption>> logger)
        {
            _option = provider.Value;

            if (!_option.TokenEndpoint.IsEnabled) return;

            if (_option.TokenEndpoint.BaseUrl == null)
                throw new ArgumentNullException(
                    $"TokenEndpoint.BaseUrl should not be null or empty. {typeof(TOption).FullName}");
            if (string.IsNullOrEmpty(_option.TokenEndpoint.Scope))
                throw new ArgumentNullException(
                    $"TokenEndpoint.Scope should not be null or empty. {typeof(TOption).FullName}");
            if (string.IsNullOrEmpty(_option.TokenEndpoint.ClientId))
                throw new ArgumentNullException(
                    $"TokenEndpoint.ClientId should not be null or empty. {typeof(TOption).FullName}");
            if (string.IsNullOrEmpty(_option.TokenEndpoint.ClientSecret))
                throw new ArgumentNullException(
                    $"TokenEndpoint.ClientSecret should not be null or empty. {typeof(TOption).FullName}");
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //todo: add token to cache

            //if (_option.TokenEndpoint.IsEnabled)
            //{
            //    var nssApi = RestService.For<IAuthApi>(_option.TokenEndpoint.BaseUrl!.ToString());
            //    var scope = _option.TokenEndpoint.Scope;
            //    var secret = _option.TokenEndpoint.ClientSecret;
            //    var data = new Dictionary<string, object>
            //    {
            //        {"grant_type", "client_credentials"},
            //        {"scope", scope},
            //        {"client_id", _option.TokenEndpoint.ClientId},
            //        {"client_secret",secret}
            //    };
            //    var logSecret = secret.Length > 0 ? secret.Substring(0, secret.Length / 5) + "..." : "...........oops secret does not binded.";
            //    _logger.LogDebug("calling NSS for getting token for information secruity. client_id: {clientId}, scope: {scope}, client_secret: {clientSecret}",
            //    _option.TokenEndpoint.ClientId, scope, logSecret);
            //    var response = await nssApi.TokenAsync(data);
            //    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", response.Token);
            //}
           
            return await base.SendAsync(request, cancellationToken);
        }
    }
}