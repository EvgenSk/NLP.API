using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NLP.API.Core;
using NLP.API.Core.Annotations;
using Orleans;
using Orleans.Concurrency;
using Orleans.Core;
using Orleans.Runtime;

namespace NLP.API.OrleansHostingExtensions
{
    [Reentrant]
    public class StanfordNLPGrainService : GrainService, IStanfordNLPGrainService
    {
        public StanfordNLPGrainService(IStanfordNLPClient stanfordNLPClient, IGrainIdentity id, Silo silo, ILoggerFactory loggerFactory)
            : base(id, silo, loggerFactory)
        {
            StanfordNLPClient = stanfordNLPClient;
        }

        public IStanfordNLPClient StanfordNLPClient { get; }

        public Task<AnnotatedText> AnnotateTextAsync(string text) =>
            StanfordNLPClient.AnnotateTextAsync(text);

        public Task<AnnotatedText> AnnotateTextAsync(string text, Annotator annotator) =>
            StanfordNLPClient.AnnotateTextAsync(text, annotator);

        public Task<string> AnnotateTextRawResultAsync(string text) =>
            StanfordNLPClient.AnnotateTextRawResultAsync(text);

        public Task<string> AnnotateTextRawResultAsync(string text, Annotator annotator, OutputFormat outputFormat = OutputFormat.JSON) =>
            StanfordNLPClient.AnnotateTextRawResultAsync(text, annotator, outputFormat);
    }
}
