using Application.Services.BankSafeDocuments.Command.AddBankSafeDocuments;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories.ISmsSevice;
using Polly;
using Polly.CircuitBreaker;
using Polly.Fallback;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SmsServiceRepositorie
{
    public class SmsServiceRepositorieQuery : ISmsServiceRepositorieQuery
    {
        private readonly AsyncRetryPolicy<OperationResult> _retryPolicy;
        private readonly AsyncFallbackPolicy<OperationResult> _fallbackPolicy;
        private static AsyncCircuitBreakerPolicy _circuitBreakerPolicy;
        private readonly HttpClient _httpClient;
        private const string Url = "6C6247593962355A50793635692B6B58344D586C6537536768484B394F5442316472454B6F66494D7941413D";
        public SmsServiceRepositorieQuery(IHttpClientFactory httpClientFactory)
        {
            _fallbackPolicy = Policy<OperationResult>.Handle<Exception>().FallbackAsync
                (OperationResult.Failure("Eror Sending Message"));

            _retryPolicy = Policy<OperationResult>.Handle<Exception>().RetryAsync(3);

            _circuitBreakerPolicy = Policy.Handle<Exception>()
                .CircuitBreakerAsync(2 , TimeSpan.FromMinutes(1));

            _httpClient = httpClientFactory.CreateClient();
        }
        public async Task<OperationResult> SendAsync
            (string apiUrl, string Receptor, string token, string token2, string token3, string template)
        {
            var result = await _circuitBreakerPolicy.ExecuteAsync(async () =>
            {
                string url = string.Format(ConstMessages.Url, apiUrl);
                var conTent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("receptor",Receptor),
                    new KeyValuePair<string, string>("token",token),
                    new KeyValuePair<string, string>("token2",token2),
                    new KeyValuePair<string, string>("token3",token3),
                    new KeyValuePair<string, string>("template",template)
                });
                return await _httpClient.PostAsync(url, conTent);
            });
            if (result.IsSuccessStatusCode)
            {
                return new OperationResult(true, null);
            }
            else
            {
                return new OperationResult(false, result.StatusCode.ToString());
            }
            
        }
    }
}
