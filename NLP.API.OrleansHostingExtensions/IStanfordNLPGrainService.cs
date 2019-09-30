using NLP.API.Core;
using NLP.API.Core.Annotations;
using Orleans.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLP.API.OrleansHostingExtensions
{
	public interface IStanfordNLPGrainService : IGrainService
	{
		Task<AnnotatedText> AnnotateTextAsync(string text);
		Task<AnnotatedText> AnnotateTextAsync(string text, Annotator annotator);
		Task<string> AnnotateTextRawResultAsync(string text);

		Task<string> AnnotateTextRawResultAsync(string text, Annotator annotator, OutputFormat outputFormat = OutputFormat.JSON);
	}
}
