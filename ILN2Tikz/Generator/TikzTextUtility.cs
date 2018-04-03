namespace ILN2Tikz.Generator
{
    public static class TikzTextUtility
    {
        public static string EscapeText(string input)
        {
            // TODO: Properly escape LaTeX string (incl. support math mode expressions)
            return input.Replace("_", " ").Replace("\\", "\\backslash").Replace("{", "\\{").Replace("}", "\\}");
        }
    }
}