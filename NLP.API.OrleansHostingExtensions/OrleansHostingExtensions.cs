using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NLP.API.Core;
using NLP.API.OrleansHostingExtensions;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Providers;
using Orleans.Runtime;
using System;

namespace Orleans.Hosting
{
	public static class OrleansHostingExtensions
	{
		private static readonly Action<StanfordNLPOptions> dummy = (_) => { };

		public static ISiloHostBuilder AddStanfordNLPClient(this ISiloHostBuilder builder, string name, Action<StanfordNLPOptions> configureOptions = null) =>
			builder.ConfigureServices(s => s.AddStanfordNLPClient(name, ob => ob.Configure(configureOptions ?? dummy)));

		public static IServiceCollection AddStanfordNLPClient(this IServiceCollection services, string name, Action<OptionsBuilder<StanfordNLPOptions>> configureOptions = null)
		{
			configureOptions?.Invoke(services.AddOptions<StanfordNLPOptions>(name));
			return
				services
				.ConfigureNamedOptionForLogging<StanfordNLPOptions>(name)
				.AddSingletonNamedService(name, StanfordNLPClientFactory.Create);
		}

		public static ISiloHostBuilder AddStanfordNLPClient(this ISiloHostBuilder builder, Action<StanfordNLPOptions> configureOptions = null) =>
			builder.ConfigureServices(s => s.AddStanfordNLPClient(ob => ob.Configure(configureOptions ?? dummy)));

		public static IServiceCollection AddStanfordNLPClient(this IServiceCollection services, Action<OptionsBuilder<StanfordNLPOptions>> configureOptions = null)
		{
			configureOptions?.Invoke(services.AddOptions<StanfordNLPOptions>());
			return services.AddSingleton<IStanfordNLPClient, StanfordNLPClient>();
		}

		public static ISiloHostBuilder AddStanfordNLPGrainService(this ISiloHostBuilder builder, Action<OptionsBuilder<StanfordNLPOptions>> configureOptions)
		{
			return
				builder
				.AddGrainService<StanfordNLPGrainService>()
				.ConfigureServices(services =>
				{
					configureOptions?.Invoke(services.AddOptions<StanfordNLPOptions>());
					services
					.AddSingleton<IStanfordNLPClient, StanfordNLPClient>()
					.AddSingleton<IStanfordNLPGrainService, StanfordNLPGrainService>()
					.AddSingleton<IStanfordNLPGrainServiceClient, StanfordNLPGrainServiceClient>();
				});
		}

		public static ISiloHostBuilder AddStanfordNLPGrainService(this ISiloHostBuilder builder, Action<StanfordNLPOptions> configureOptions = null) =>
			builder.AddStanfordNLPGrainService(ob => ob.Configure(configureOptions ?? dummy));
	}
}
