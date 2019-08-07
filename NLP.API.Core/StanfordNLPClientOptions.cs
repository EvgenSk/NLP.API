using System;
using System.Collections.Generic;
using System.Text;

namespace NLP.API.Core
{
	public class StanfordNLPClientOptions
	{
        /// <summary>
        /// Default is "localhost:9000"
        /// </summary>
        public string ConnectionString { get; set; } = "localhost:9000";

        /// <summary>
        /// Default is Annotator.Default == Tokenize | Ssplit | Lemma | Lemma | POS
        /// </summary>
        public Annotator Annotator { get; set; } = Annotator.Default;

        /// <summary>
        /// Default is JSON
        /// </summary>
        public OutputFormat OutputFormat { get; set; } = OutputFormat.JSON;
    }
}
