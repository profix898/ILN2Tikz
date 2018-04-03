using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ILN2Tikz.Generator.Global;
using ILNumerics;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;

namespace ILN2Tikz.Generator.Elements
{
    public class TikzPlot : ITikzElement
    {
        private Globals globals;
        private ILLinePlot linePlot;

        #region Implementation of ITikzElement

        public string PreTag
        {
            get
            {
                if (MarkerStyle == MarkerStyle.None)
                    return $"\\addplot[{TikzFormatUtility.FormatLine(globals, LineColor, LineStyle, LineWidth)}]";

                return $"\\addplot[{TikzFormatUtility.FormatLine(globals, LineColor, LineStyle, LineWidth)},{TikzFormatUtility.FormatMarker(globals, MarkerColor, MarkerStyle, MarkerSize)}]";
            }
        }

        public IEnumerable<string> Content
        {
            get
            {
                if (linePlot != null)
                {
                    foreach (var tableEntry in FormatDataTable(linePlot))
                        yield return tableEntry;
                }
            }
        }

        public string PostTag
        {
            get
            {
                if (String.IsNullOrEmpty(LegendItemText))
                    return "";

                return $"\\addlegendentry{{{TikzTextUtility.EscapeText(LegendItemText)}}}";
            }
        }

        public void Bind(ILNode node, Globals globals)
        {
            this.globals = globals;

            if (!(node is ILLinePlot linePlot))
                return;

            this.linePlot = linePlot; // Reference for data table

            // Line
            LineColor = linePlot.Line.Color ?? Color.Black;
            globals.Colors.Add(LineColor);
            LineStyle = linePlot.Line.DashStyle;
            LineWidth = linePlot.Line.Width;

            // Marker
            MarkerColor = linePlot.Marker.Fill.Color ?? LineColor;
            globals.Colors.Add(MarkerColor);
            MarkerStyle = linePlot.Marker.Style;
            MarkerSize = Math.Max(linePlot.Marker.Size / 2, 1);

            // LegendEntry
            var legend = linePlot.FirstUp<ILPlotCube>().First<ILLegend>();
            if (legend != null)
                LegendItemText = legend.Find<ILLegendItem>().FirstOrDefault(legendItem => legendItem.ProviderID == linePlot.ID)?.Text;
        }

        #endregion

        #region Properties

        #region Line

        public Color LineColor { get; set; }

        public DashStyle LineStyle { get; set; }

        public int LineWidth { get; set; }

        #endregion

        #region Marker

        public Color MarkerColor { get; set; }

        public MarkerStyle MarkerStyle { get; set; }

        public int MarkerSize { get; set; }

        #endregion

        #region Legend

        public string LegendItemText { get; set; }

        #endregion

        #endregion

        #region Helpers

        private static IEnumerable<string> FormatDataTable(ILLinePlot linePlot)
        {
            var scaleModes = linePlot.FirstUp<ILPlotCubeDataGroup>().ScaleModes;
            
            yield return "  table[row sep=crcr]{";

            ILArray<float> positions = linePlot.Positions; // 3 x n
            for (var i = 0; i < positions.S[1]; i++)
            {
                ILArray<float> xyz = positions[ILMath.full, i];
                var x = (float) xyz[0];
                if (scaleModes.XAxisScale == AxisScale.Logarithmic)
                    x = (float) Math.Pow(10.0, x);
                var y = (float) xyz[1];
                if (scaleModes.YAxisScale == AxisScale.Logarithmic)
                    y = (float) Math.Pow(10.0, y);

                yield return FormattableString.Invariant($"  {x}	{y}\\\\");
            }

            yield return "};";
        }

        #endregion
    }
}
