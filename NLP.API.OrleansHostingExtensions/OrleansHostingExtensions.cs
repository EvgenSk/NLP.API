using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NLP.API.Core;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Providers;
using Orleans.Runtime;
using System;

namespace Orleans.Hosting
{
	public static class OrleansHostingExtensions
	{
        private static readonly Action<StanfordNLPClientOptions> dummy = (_) => { };

		public static ISiloHostBuilder AddStanfordNLPClient(this ISiloHostBuilder builder, string name, Action<StanfordNLPClientOptions> configureOptions = null) =>
			builder.ConfigureServices(s => s.AddStanfordNLPClient(name, ob => ob.Configure(configureOptions ?? dummy)));

		public static IServiceCollection AddStanfordNLPClient(this IServiceCollection services, string name, Action<OptionsBuilder<StanfordNLPClientOptions>> configureOptions = null)
		{
			configureOptions?.Invoke(services.AddOptions<StanfordNLPClientOptions>(name));
			return
				services
				.ConfigureNamedOptionForLogging<StanfordNLPClientOptions>(name)
				.AddSingletonNamedService(name, StanfordNLPClientFactory.Create);
		}

        public static ISiloHostBuilder AddStanfordNLPClient(this ISiloHostBuilder builder, Action<StanfordNLPClientOptions> configureOptions = null) =>
            builder.ConfigureServices(s => s.AddStanfordNLPClient(ob => ob.Configure(configureOptions ?? dummy)));

        public static IServiceCollection AddStanfordNLPClient(this IServiceCollection services, Action<OptionsBuilder<StanfordNLPClientOptions>> configureOptions = null)
        {
            configureOptions?.Invoke(services.AddOptions<StanfordNLPClientOptions>());
            return services.AddSingleton(StanfordNLPClientFactory.Create);
        }

    }
}
