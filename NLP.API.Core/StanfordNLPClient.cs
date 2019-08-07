using NLP.API.Core.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace NLP.API.Core
{
	public class StanfordNLPClient : IStanfordNLPClient
	{
		private StanfordNLPClientOptions Options { get; }

		public StanfordNLPClient(StanfordNLPClientOptions options)
		{
			Options = options;
		}

		static List<Annotator> AllAnnotators { get; } = Enum.GetValues(typeof(Annotator)).Cast<Annotator>().ToList();

        /// <summary>
        /// Processes given text using annotators from options
        /// </summary>
        /// <param name="text">Text to be annotated</param>
        /// <returns>Text, annotated with given annotators</returns>
        public Task<AnnotatedText> AnnotateTextAsync(string text) =>
            AnnotateTextAsync(text, Options?.Annotator ?? Annotator.Default);

        /// <summary>
        /// Processes given text using selected annotators
        /// </summary>
        /// <param name="text">Text to be annotated</param>
        /// <param name="annotator">Annotators - flags - should be connected via |</param>
        /// <returns>Text, annotated with given annotators</returns>
        public async Task<AnnotatedText> AnnotateTextAsync(string text, Annotator annotator)
		{
			string rawText = await AnnotateTextRawResultAsync(text, annotator, OutputFormat.JSON);
			return
				string.IsNullOrEmpty(rawText)
				? null
				: JsonConvert.DeserializeObject<AnnotatedText>(rawText);
		}

        /// <summary>
        /// Processes given text using annotators and output format parameters from options
        /// </summary>
        /// <param name="text">Text to be annotated</param>
        /// <returns>string output from Stanford NLP service in form of selected output format</returns>
        public Task<string> AnnotateTextRawResultAsync(string text) =>
            AnnotateTextRawResultAsync(text, Options?.Annotator ?? Annotator.Default, Options?.OutputFormat ?? OutputFormat.JSON);

        /// <summary>
        /// Processes given text using selected annotators
        /// </summary>
        /// <param name="text">Text to be processed</param>
        /// <param name="annotator">Annotators - flags - should be connected via |</param>
        /// <param name="outputFormat">Output Format - one of [JSON, XML, Text]</param>
        /// <returns>string output from Stanford NLP service in form of selected output format</returns>
        public async Task<string> AnnotateTextRawResultAsync(string text, Annotator annotator, OutputFormat outputFormat = OutputFormat.JSON)
		{
			using (var httpClient = new HttpClient())
			{
				string queryString = $"{Options.ConnectionString}/?{GetPropertiesString(annotator, outputFormat)}";
				var response = await httpClient.PostAsync(queryString, new StringContent(text));
				return
					response.StatusCode == HttpStatusCode.OK
					? await response.Content.ReadAsStringAsync()
					: string.Empty;
			}
		}

		string GetPropertiesString(Annotator annotator, OutputFormat outputFormat)
		{
			var annotators = AllAnnotators.Select(a => annotator & a).Where(a => a != Annotator.None);
			return GetPropertiesString(annotators, outputFormat);
		}

		string GetPropertiesString(IEnumerable<Annotator> annotators, OutputFormat outputFormat)
		{
			var strings = annotators.Select(a => a.ToString().ToLower());
			var stringAnnotators = string.Join(",", strings);

			string parametersJson = $"\"annotators\":\"{stringAnnotators}\",\"outputFormat\":\"{outputFormat.ToString().ToLowerInvariant()}\"";
			return "properties={" + parametersJson + "}";
		}
	}

	public enum OutputFormat { JSON, XML, Text }

	[Flags]
	public enum Annotator
	{
		None = 0,
		Tokenize = 1,
		Ssplit = 2,
		POS = 4,
		Lemma = 8,
		NER = 16,
		RegexNER = 32,
        Default = Tokenize | Ssplit | Lemma | Lemma | POS,
        All = Tokenize | Ssplit | POS | Lemma | NER | RegexNER
	}

	public static class StanfordNLPClientFactory
	{
		public static StanfordNLPClient Create(IServiceProvider services)
		{
			IOptionsSnapshot<StanfordNLPClientOptions> optionsSnapshot = services.GetRequiredService<IOptionsSnapshot<StanfordNLPClientOptions>>();
			return ActivatorUtilities.CreateInstance<StanfordNLPClient>(services, optionsSnapshot);
		}

		public static StanfordNLPClient Create(IServiceProvider services, string name)
		{
			IOptionsSnapshot<StanfordNLPClientOptions> optionsSnapshot = services.GetRequiredService<IOptionsSnapshot<StanfordNLPClientOptions>>();
			return ActivatorUtilities.CreateInstance<StanfordNLPClient>(services, optionsSnapshot.Get(name));
		}
	}
}
