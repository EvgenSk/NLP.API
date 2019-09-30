namespace NLP.API.Core.Annotations
{
	public class Token
	{
		public int index { get; set; }
		public string word { get; set; }
		public string originalText { get; set; }
		public string lemma { get; set; }
		public int characterOffsetBegin { get; set; }
		public int characterOffsetEnd { get; set; }
		public PartOfSpeech? pos { get; set; }
		public string before { get; set; }
		public string after { get; set; }
	}
}