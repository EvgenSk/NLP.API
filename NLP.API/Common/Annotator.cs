using System;
using System.Collections.Generic;
using System.Linq;

namespace NLP.API.Common
{
	public static class Annotator
	{
		[Flags]
		public enum Type
		{
			None = 0,
			Tokenize = 1,
			Ssplit = 2,
			POS = 4,
			Lemma = 8,
			NER = 16,
			RegexNER = 32
		}

		public static List<Type> AllAnnotators { get; } = Enum.GetValues(typeof(Type)).Cast<Type>().ToList();
	}
}
