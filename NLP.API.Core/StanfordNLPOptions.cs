using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace NLP.API.Core
{
	public class StanfordNLPOptions : IOptions<StanfordNLPOptions>
	{
		/// <summary>
		/// Default is "localhost:9000"
		/// </summary>
		public string ConnectionString { get; set; } = "https://localhost:9000";

		/// <summary>
		/// Default is Annotator.Default == Tokenize | Ssplit | Lemma | POS
		/// </summary>
		public Annotator Annotator { get; set; } = StanfordNLPClient.DefaultAnnotator;

		/// <summary>
		/// Default is JSON
		/// </summary>
		public OutputFormat OutputFormat { get; set; } = OutputFormat.JSON;

		StanfordNLPOptions IOptions<StanfordNLPOptions>.Value => this;
	}
}
