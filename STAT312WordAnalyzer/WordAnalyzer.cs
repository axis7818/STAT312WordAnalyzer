namespace STAT312WordAnalyzer
{
    public static class WordAnalyzer
    {
        public static float WordComplexity(Word word)
        {
            return (float)word.UniqueChars / (float)word.Length;
        }
    }
}
