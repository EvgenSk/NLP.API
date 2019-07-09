using System.Collections.Generic;

namespace NLP.API.Common.Annotations
{
    public class Sentence
    {
        public int Index { get; set; }
        public List<Token> Tokens { get; set; }
    }
}