using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NLP.API.Common
{
	public class StanfordNLPClient
	{
		public StanfordNLPClientOptions Options { get; }

		public StanfordNLPClient(StanfordNLPClientOptions options)
		{
			Options = options;
		}

		string EndPoint { get => Options.Port == 0 ? $"{Options.Host}" : $"{Options.Host}:{Options.Port}"; }

		/// <summary>
		/// Processes given text using selected annotators
		/// </summary>
		/// <param name="text">Text to be processed</param>
		/// <param name="annotator">Annotators - flags - should be connected via |</param>
		/// <returns>JSON output from Stanford NLP service</returns>
		public async Task<string> ProcessAsync(string text, Annotator.Type annotator)
		{
			string queryString = $"{EndPoint}/?{GetPropertiesString(annotator)}";
			using (var httpClient = new HttpClient())
			{
				var response = await httpClient.PostAsync(queryString, new StringContent(text));
				var responseStringTask = response.Content.ReadAsStringAsync();
				if (response.StatusCode == HttpStatusCode.OK)
				{
					return await responseStringTask;
				}
				else
				{
					return string.Empty;
				}
			}
		}
		string GetPropertiesString(IEnumerable<Annotator.Type> annotators)
		{
			var strings = annotators.Select(a => a.ToString().ToLower());
			var stringAnnotators = string.Join(",", strings);

			string parametersJson = $"\"annotators\":\"{stringAnnotators}\",\"outputFormat\":\"JSON\"";
			return "properties={" + parametersJson + "}";
		}

		string GetPropertiesString(Annotator.Type annotator)
		{
			var annotators = Annotator.AllAnnotators.Select(a => annotator & a).Where(a => a != Annotator.Type.None);
			return GetPropertiesString(annotators);
		}
	}
}
