using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NLP.API.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Hosting
{
    public static class WebHostingExtensions
    {
        public static IWebHostBuilder AddStanfordNLPClient(this IWebHostBuilder hostBuilder, Action<StanfordNLPClientOptions> configureOptions = null) =>
            hostBuilder.ConfigureServices(s => s.AddStanfordNLPClient(ob => ob.Configure(configureOptions)));

        public static IServiceCollection AddStanfordNLPClient(this IServiceCollection services, Action<OptionsBuilder<StanfordNLPClientOptions>> configureOptions = null)
        {
            configureOptions?.Invoke(services.AddOptions<StanfordNLPClientOptions>());
            return services.AddSingleton<IStanfordNLPClient, StanfordNLPClient>(StanfordNLPClientFactory.Create);
        }
    }
}
