using System.Text.RegularExpressions;

namespace ILN2Tikz.Generator
{
    public static class TikzTextUtility
    {
        public static string EscapeText(string input)
        {
            // Detect basic math sequences "a_{a}" and "a^{b}"
            input = Regex.Replace(input, @"(.*)\b([\w\d]+_[\w\d]{1})\b(.*)", @"$1$$$2$$$3"); // "a_a"
            input = Regex.Replace(input, @"(.*)\b([\w\d]+\^[\w\d]{1})\b(.*)", @"$1$$$2$$$3"); // "a^a"
            input = Regex.Replace(input, @"([\w\d]+_\{[\w\d]+\})", @"$$$1$$"); // "a_{abc}"
            input = Regex.Replace(input, @"([\w\d]+\^\{[\w\d]+\})", @"$$$1$$"); // "a^{abc}"

            // Replace '{', '}', '_', '^' (if not inside math environment)
            input = Regex.Replace(input, @"(?!\B\$[^\$]*)(\{)(?![^\$]*\$\B)", @"\$1"); // '{'
            input = Regex.Replace(input, @"(?!\B\$[^\$]*)(\})(?![^\$]*\$\B)", @"\$1"); // '}'
            input = Regex.Replace(input, @"(?!\B\$[^\$]*)(_)(?![^\$]*\$\B)", @"\$1"); // '_'
            input = Regex.Replace(input, @"(?!\B\$[^\$]*)(\^)(?![^\$]*\$\B)", @"\$1"); // '^'

            // Replace '\' (if not used for escaping above)
            input = Regex.Replace(input, @"\\(?=[^\{\}_\^])", @"\\"); // '\'

            return input;
        }
    }
}
