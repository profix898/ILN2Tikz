using System.Collections.Generic;
using ILNumerics.Drawing.Plotting;

namespace ILN2Tikz.Generator.Plots
{
    public class TikzPlot : ITikzElement
    {
        public TikzPlot(ILLinePlot linePlot)
        {
            // TODO: Bind child objects
        }

        #region Implementation of ITikzElement

        public string PreTag
        {
            get { return @"\addplot["; }
        }

        public IEnumerable<string> Content
        {
            get { throw new System.NotImplementedException(); }
        }

        public string PostTag
        {
            get { return "]"; }
        }

        #endregion
    }
}
