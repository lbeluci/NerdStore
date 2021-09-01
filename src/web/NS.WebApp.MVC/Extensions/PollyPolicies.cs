using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using System;
using System.Net.Http;

namespace NS.WebApp.MVC.Extensions
{
    public static class PollyPolicies
    {
        public static AsyncRetryPolicy<HttpResponseMessage> GetAsyncRetryPolicy()
        {
            return HttpPolicyExtensions.HandleTransientHttpError()
                .WaitAndRetryAsync(new[] 
                { 
                    TimeSpan.FromSeconds(1), 
                    TimeSpan.FromSeconds(2), 
                    TimeSpan.FromSeconds(4) 
                }, (outcome, timespan, retryCount, context) => 
                { 
                    Console.ForegroundColor = ConsoleColor.Red; 
                    Console.WriteLine($"Trying {retryCount} times!"); 
                    Console.ForegroundColor = ConsoleColor.White; 
                });
        }

        public static Func<PolicyBuilder<HttpResponseMessage>, IAsyncPolicy<HttpResponseMessage>> GetCircuitBreakerAsync()
        {
            return p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }
    }
}