using System.Collections.Generic;
using ILNumerics.Drawing;
using TikzExport.Generator.Global;

namespace TikzExport.Generator;

public interface ITikzElement
{
    #region TikzTags

    string PreTag { get; }

    IEnumerable<string> Content { get; }

    string PostTag { get; }

    #endregion

    #region Binding
        
    void Bind(Node node, TikzGlobals globals);

    #endregion
}