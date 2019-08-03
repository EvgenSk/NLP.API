using NLP.API.Core.Annotations;
using System.Threading.Tasks;

namespace NLP.API.Core
{
	public interface IStanfordNLPClient
	{
        Task<AnnotatedText> AnnotateTextAsync(string text);
        Task<AnnotatedText> AnnotateTextAsync(string text, Annotator annotator);
        Task<string> AnnotateTextRawResultAsync(string text);

        Task<string> AnnotateTextRawResultAsync(string text, Annotator annotator, OutputFormat outputFormat = OutputFormat.JSON);
	}
}