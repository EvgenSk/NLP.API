using NLP.API.Core.Annotations;
using System.Threading.Tasks;

namespace NLP.API.Core
{
	public interface IStanfordNLPClient
	{
		Task<AnnotatedText> ProcessTextAsync(string text, Annotator annotator);
		Task<string> ProcessTextRawResultAsync(string text, Annotator annotator, OutputFormat outputFormat = OutputFormat.JSON);
	}
}