using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ILN2Tikz.Generator.Global;
using ILNumerics.Drawing;
using static ILN2Tikz.Generator.TikzFormatUtility;

namespace ILN2Tikz.Generator.Elements
{
    public class TikzPlot : ITikzElement
    {
        protected TikzGlobals globals;
        protected LinePlot linePlot;

        #region Implementation of ITikzElement

        public virtual string PreTag
        {
            get
            {
                var lineStyle = FormatLine(globals, LineColor, LineStyle, LineWidth);

                if (MarkerStyle == MarkerStyle.None)
                    return $"\\addplot[{lineStyle}]";

                var markerStyle = FormatMarker(globals, MarkerColor, MarkerStyle, MarkerSize);

                return $"\\addplot[{lineStyle},{markerStyle}]";
            }
        }

        public virtual IEnumerable<string> Content
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

        public virtual string PostTag
        {
            get
            {
                if (String.IsNullOrEmpty(LegendItemText))
                    return "";

                return $"\\addlegendentry{{{TikzTextUtility.EscapeText(LegendItemText)}}}";
            }
        }

        public virtual void Bind(Node node, TikzGlobals globals)
        {
            this.globals = globals;

            if (!(node is LinePlot linePlot))
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
            var legend = linePlot.FirstUp<PlotCube>().First<Legend>();
            if (legend != null)
                LegendItemText = legend.Find<LegendItem>().FirstOrDefault(legendItem => legendItem.ProviderID == linePlot.ID)?.Text;
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

        private static IEnumerable<string> FormatDataTable(LinePlot linePlot)
        {
            var scaleModes = linePlot.FirstUp<PlotCubeDataGroup>().ScaleModes;

            yield return "  table[x=x, y=y, row sep=crcr]{";
            yield return "  x	y\\\\"; // Header

            Array<float> positions = linePlot.Positions; // 3 x n
            for (var i = 0; i < positions.S[1]; i++)
            {
                Array<float> xyz = positions[full, i];
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
