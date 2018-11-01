using System;
using System.Collections.Generic;
using ILNumerics;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;

namespace ILN2Tikz.Generator.Elements
{
    public class TikzPlot3 : ITikzElement
    {
        private Surface surface;

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

        public void Bind(Node node, Global.Globals globals)
        {
            if (!(node is Surface surface))
                return;

            this.surface = surface; // Reference for data table

            // TODO: Colormap, etc.
        }

        #endregion

        #region FormatHelper

        private static IEnumerable<string> DataTable(Surface surface)
        {
            var scaleModes = surface.FirstUp<PlotCubeDataGroup>().ScaleModes;

            yield return "  table[row sep=crcr]{";

            Array<float> positions = surface.Positions; // n x n x 3 (z, x, y)
            for (var i = 0; i < positions.S[0]; i++)
            {
                for (var j = 0; j < positions.S[1]; j++)
                {
                    Array<float> xyz = positions[i, j, Globals.full];
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