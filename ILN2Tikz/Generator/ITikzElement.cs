using System.Collections.Generic;
using ILNumerics.Drawing;

namespace ILN2Tikz.Generator
{
    public interface ITikzElement
    {
        #region TikzTags

        string PreTag { get; }

        IEnumerable<string> Content { get; }

        string PostTag { get; }

        #endregion

        #region Binding
        
        void Bind(ILNode node);

        #endregion
    }
}
