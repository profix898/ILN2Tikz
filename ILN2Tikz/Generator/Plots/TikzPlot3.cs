using System.Collections.Generic;
using ILNumerics.Drawing;

namespace ILN2Tikz.Generator.Plots
{
    public class TikzPlot3 : ITikzElement
    {
        #region Implementation of ITikzElement

        public string PreTag
        {
            get { return @"\addplot3["; }
        }

        public IEnumerable<string> Content
        {
            get { throw new System.NotImplementedException(); }
        }

        public string PostTag
        {
            get { return "]"; }
        }

        public void Bind(ILNode node)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}