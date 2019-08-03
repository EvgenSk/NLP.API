using System;
using System.Collections.Generic;
using System.Text;

namespace NLP.API.Core
{
	public class StanfordNLPClientOptions
	{
		public string Host { get; set; }
		public int Port { get; set; }
        public Annotator Annotator { get; set; } = Annotator.Default;
        public OutputFormat OutputFormat { get; set; } = OutputFormat.JSON;
    }
}
