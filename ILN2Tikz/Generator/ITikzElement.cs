using System.Collections.Generic;

namespace ILN2Tikz.Generator
{
    public interface ITikzElement
    {
        string PreTag { get; }

        IEnumerable<string> Content { get; }

        string PostTag { get; }
    }
}
