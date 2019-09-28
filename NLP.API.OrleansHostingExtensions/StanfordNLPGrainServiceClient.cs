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
        public StanfordNLPGrainServiceClient(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        public Task<AnnotatedText> AnnotateTextAsync(string text) =>
            GrainService.AnnotateTextAsync(text);

        public Task<AnnotatedText> AnnotateTextAsync(string text, Annotator annotator) =>
            GrainService.AnnotateTextAsync(text, annotator);

        public Task<string> AnnotateTextRawResultAsync(string text) =>
            GrainService.AnnotateTextRawResultAsync(text);

        public Task<string> AnnotateTextRawResultAsync(string text, Annotator annotator, OutputFormat outputFormat = OutputFormat.JSON) =>
            AnnotateTextRawResultAsync(text, annotator, outputFormat);
    }
}
