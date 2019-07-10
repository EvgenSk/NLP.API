using NLP.API.Common.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NLP.API.Common
{
	public class StanfordNLPClient
	{
		public StanfordNLPClientOptions Options { get; }

		public StanfordNLPClient(StanfordNLPClientOptions options)
		{
			Options = options;
		}

		string EndPoint => Options.Port == 0 ? $"{Options.Host}" : $"{Options.Host}:{Options.Port}";
		static List<Annotator> AllAnnotators { get; } = Enum.GetValues(typeof(Annotator)).Cast<Annotator>().ToList();

		/// <summary>
		/// Processes given text using selected annotators
		/// </summary>
		/// <param name="text">Text to be processed</param>
		/// <param name="annotator">Annotators - flags - should be connected via |</param>
		/// <returns>Text, annotated with given annotators</returns>
		public async Task<AnnotatedText> ProcessTextAsync(string text, Annotator annotator)
		{
			string rawText = await ProcessTextRawResultAsync(text, annotator, OutputFormat.JSON);
			return 
				string.IsNullOrEmpty(rawText)
				? null
				: JsonConvert.DeserializeObject<AnnotatedText>(rawText);
		}

		/// <summary>
		/// Processes given text using selected annotators
		/// </summary>
		/// <param name="text">Text to be processed</param>
		/// <param name="annotator">Annotators - flags - should be connected via |</param>
		/// <param name="outputFormat">Output Format - one of [JSON, XML, Text]</param>
		/// <returns>JSON output from Stanford NLP service</returns>
		public async Task<string> ProcessTextRawResultAsync(string text, Annotator annotator, OutputFormat outputFormat = OutputFormat.JSON)
		{
			using (var httpClient = new HttpClient())
			{
				string queryString = $"{EndPoint}/?{GetPropertiesString(annotator, outputFormat)}";
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
		RegexNER = 32
	}
}
