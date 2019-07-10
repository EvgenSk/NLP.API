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
		public static ISiloHostBuilder AddStanfordNLPClient(this ISiloHostBuilder builder, Action<StanfordNLPClientOptions> configureOptions) =>
			builder.AddStanfordNLPClient(ProviderConstants.DEFAULT_STORAGE_PROVIDER_NAME, configureOptions);

		public static ISiloHostBuilder AddStanfordNLPClient(this ISiloHostBuilder builder, string name, Action<StanfordNLPClientOptions> configureOptions) =>
			builder.ConfigureServices(s => s.AddStanfordNLPClient(name, ob => ob.Configure(configureOptions)));

		public static IServiceCollection AddStanfordNLPClient(this IServiceCollection services, string name, Action<OptionsBuilder<StanfordNLPClientOptions>> configureOptions = null)
		{
			configureOptions?.Invoke(services.AddOptions<StanfordNLPClientOptions>(name));
			return 
				services
				.ConfigureNamedOptionForLogging<StanfordNLPClientOptions>(name)
				.AddSingletonNamedService(name, StanfordNLPClientFactory.Create);
		}
	}
}
