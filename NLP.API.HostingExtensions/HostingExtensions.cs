using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NLP.API.Core;
using System;

namespace Microsoft.Extensions.Hosting
{
	public static class HostingExtensions
	{
		public static IHostBuilder AddStanfordNLPClient(this IHostBuilder hostBuilder, Action<StanfordNLPClientOptions> configureOptions) =>
			hostBuilder.ConfigureServices(s => s.AddStanfordNLPClient(ob => ob.Configure(configureOptions)));

		public static IServiceCollection AddStanfordNLPClient(this IServiceCollection services, Action<OptionsBuilder<StanfordNLPClientOptions>> configureOptions = null)
		{
			configureOptions?.Invoke(services.AddOptions<StanfordNLPClientOptions>());
			return services.AddSingleton<IStanfordNLPClient,StanfordNLPClient>(StanfordNLPClientFactory.Create);
		}
	}
}
