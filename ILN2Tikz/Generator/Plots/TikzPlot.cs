using System;
using System.Collections.Generic;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;

namespace ILN2Tikz.Generator.Plots
{
    public class TikzPlot : ITikzElement
    {
        private ILLinePlot linePlot;

        #region Implementation of ITikzElement

        public string PreTag
        {
            get { return @"\addplot[color=blue,solid,thick]"; }
        }

        public IEnumerable<string> Content
        {
            get
            {
                if (linePlot != null)
                {
                    foreach (var tableEntry in TableHelper.TikzTable(linePlot))
                        yield return tableEntry;
                }
            }
        }

        public string PostTag
        {
            get { return ""; }
        }

        public void Bind(ILNode node)
        {
            var linePlot = node as ILLinePlot;
            if (linePlot == null)
                return;

            this.linePlot = linePlot; // Reference for data table

            // TODO: Color, line style, line width, etc.
        }

        #endregion
    }
}
