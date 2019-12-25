using NLP.API.Core;
using NLP.API.Core.Annotations;
using Orleans.Runtime.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLP.API.OrleansHostingExtensions
{
	public class StanfordNLPGrainServiceClient : GrainServiceClient<IStanfordNLPGrainService>, IStanfordNLPGrainServiceClient
	{
		private readonly IStanfordNLPClient client;

		public StanfordNLPGrainServiceClient(IServiceProvider serviceProvider, IStanfordNLPClient client)
			: base(serviceProvider)
		{
			this.client = client;
		}

		public Task<AnnotatedText> AnnotateTextAsync(string text) =>
			client.AnnotateTextAsync(text);

		public Task<AnnotatedText> AnnotateTextAsync(string text, Annotator annotator) =>
			client.AnnotateTextAsync(text, annotator);

		public Task<string> AnnotateTextRawResultAsync(string text) =>
			client.AnnotateTextRawResultAsync(text);

		public Task<string> AnnotateTextRawResultAsync(string text, Annotator annotator, OutputFormat outputFormat = OutputFormat.JSON) =>
			client.AnnotateTextRawResultAsync(text, annotator, outputFormat);
	}
}
