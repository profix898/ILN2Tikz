using System;
using System.Collections.Generic;
using ILN2Tikz.Generator.Global;
using ILNumerics;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;

namespace ILN2Tikz.Generator.Elements
{
    public class TikzPlot3 : ITikzElement
    {
        private ILSurface surface;

        #region Implementation of ITikzElement

        public string PreTag
        {
            get { return @"\addplot3[surf,shader=interp]"; }
        }

        public IEnumerable<string> Content
        {
            get
            {
                if (surface != null)
                {
                    foreach (var tableEntry in DataTable(surface))
                        yield return tableEntry;
                }
            }
        }

        public string PostTag
        {
            get { return ""; }
        }

        public void Bind(ILNode node, Globals globals)
        {
            if (!(node is ILSurface surface))
                return;

            this.surface = surface; // Reference for data table

            // TODO: Colormap, etc.
        }

        #endregion

        #region FormatHelper

        private static IEnumerable<string> DataTable(ILSurface surface)
        {
            var scaleModes = surface.FirstUp<ILPlotCubeDataGroup>().ScaleModes;

            yield return "  table[row sep=crcr]{";

            ILArray<float> positions = surface.Positions; // n x n x 3 (z, x, y)
            for (var i = 0; i < positions.S[0]; i++)
            {
                for (var j = 0; j < positions.S[1]; j++)
                {
                    ILArray<float> xyz = positions[i, j, ILMath.full];
                    var x = (float) xyz[1];
                    if (scaleModes.XAxisScale == AxisScale.Logarithmic)
                        x = (float) Math.Pow(10.0, x);
                    var y = (float) xyz[2];
                    if (scaleModes.YAxisScale == AxisScale.Logarithmic)
                        y = (float) Math.Pow(10.0, y);
                    var z = (float) xyz[0];
                    if (scaleModes.ZAxisScale == AxisScale.Logarithmic)
                        z = (float) Math.Pow(10.0, z);

                    yield return FormattableString.Invariant($"  {x}  {y} {z}\\\\");
                }
            }

            yield return "};";
        }

        #endregion
    }
}