using System;
using System.Collections.Generic;
using ILN2Tikz.Generator.Global;
using ILNumerics.Drawing;
using static ILN2Tikz.Generator.TikzFormatUtility;

namespace ILN2Tikz.Generator.Elements
{
    public class TikzPlotPlus : TikzPlot
    {
        private ErrorBarPlot errorBarPlot;

        #region Implementation of ITikzElement

        public override string PreTag
        {
            get
            {
                var lineStyle = FormatLine(globals, LineColor, LineStyle, LineWidth);
                var errorBars = FormatErrorBars();

                if (MarkerStyle == MarkerStyle.None)
                    return $"\\addplot+[{lineStyle},{errorBars}]";

                var markerStyle = FormatMarker(globals, MarkerColor, MarkerStyle, MarkerSize);

                return $"\\addplot+[{lineStyle},{markerStyle},{errorBars}]";
            }
        }

        public override IEnumerable<string> Content
        {
            get
            {
                if (linePlot != null)
                {
                    foreach (var tableEntry in FormatErrorDataTable(errorBarPlot))
                        yield return tableEntry;
                }
            }
        }

        public override void Bind(Node node, TikzGlobals globals)
        {
            this.globals = globals;

            if (!(node is ErrorBarPlot errorBarPlot))
                return;

            this.errorBarPlot = errorBarPlot; // Reference for data table
            
            // Delegate LinePlot to TikzPlot
            base.Bind(errorBarPlot.LinePlot, globals);
        }

        #endregion

        #region Helpers

        private static string FormatErrorBars()
        {
            // TODO: Error bar style
            return "error bars/.cd, y fixed,y dir=both, y explicit";
        }

        private static IEnumerable<string> FormatErrorDataTable(ErrorBarPlot errorBarPlot)
        {
            var scaleModes = errorBarPlot.FirstUp<PlotCubeDataGroup>().ScaleModes;

            yield return "  table[x=x, y=y, y error=ye, row sep=crcr]{";
            yield return "  x	y  ye\\\\"; // Header

            Array<float> linePlotPositions = errorBarPlot.LinePlot.Positions; // 3 x n
            Array<float> errorBarPositions = errorBarPlot.ErrorBar.Positions.Storage; // 3 x (3 * 2 * n)
            for (var i = 0; i < linePlotPositions.S[1]; i++)
            {
                // Line plot
                Array<float> xyz = linePlotPositions[full, i];
                var x = (float) xyz[0];
                if (scaleModes.XAxisScale == AxisScale.Logarithmic)
                    x = (float) Math.Pow(10.0, x);
                var y = (float) xyz[1];
                if (scaleModes.YAxisScale == AxisScale.Logarithmic)
                    y = (float) Math.Pow(10.0, y);

                // Error bars
                var yeLower = 0f;
                var yeUpper = 0f;
                for (var j = 0; j < 3; j++) // Pairs of position = one line
                {
                    if (j == 0) // Vertical line
                        continue;
                    if (j == 1) // Lower error bar
                        yeLower = errorBarPositions.GetValue(1L, (6 * i) + (2 * j));
                    if (j == 2) // Upper error bar
                        yeUpper = errorBarPositions.GetValue(1L, (6 * i) + (2 * j));
                }

                if (scaleModes.YAxisScale == AxisScale.Logarithmic)
                    yeLower = (float) Math.Pow(10.0, yeLower);
                if (scaleModes.YAxisScale == AxisScale.Logarithmic)
                    yeUpper = (float) Math.Pow(10.0, yeUpper);

                // Note: TIKZ does not support different upper/lower errors (-> use largest error margin in export)
                var ye = Math.Max(Math.Abs(yeLower - y), Math.Abs(yeUpper - y));

                yield return FormattableString.Invariant($"  {x}	{y}  {ye}\\\\");
            }

            yield return "};";
        }

        #endregion
    }
}
