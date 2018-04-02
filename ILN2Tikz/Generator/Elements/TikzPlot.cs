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
        private ILLinePlot linePlot;

        #region Implementation of ITikzElement

        public string PreTag
        {
            get
            {
                if (MarkerStyle == MarkerStyle.None)
                    return $"\\addplot[{FormatTikzLine}]";

                return $"\\addplot[{FormatTikzLine},{FormatTikzMarker}]";
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

                return $"\\addlegendentry{{{LegendItemText}}}";
            }
        }

        public void Bind(ILNode node)
        {
            if (!(node is ILLinePlot linePlot))
                return;

            this.linePlot = linePlot; // Reference for data table

            // Line
            LineColor = linePlot.Line.Color ?? Color.Black;
            Globals.Colors.Add(LineColor);
            LineStyle = linePlot.Line.DashStyle;
            LineWidth = linePlot.Line.Width;

            // Marker
            MarkerColor = linePlot.Marker.Fill.Color ?? LineColor;
            Globals.Colors.Add(MarkerColor);
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

        #region FormatHelpers

        #region Line

        private string FormatTikzLine
        {
            get { return $"color={Globals.Colors.GetColorName(LineColor)},{FormatTikzDashStyle},line width={LineWidth}pt"; }
        }

        private string FormatTikzDashStyle
        {
            get
            {
                switch (LineStyle)
                {
                    case DashStyle.Solid:
                        return "solid";
                    case DashStyle.Dashed:
                        return "dashed";
                    case DashStyle.PointDash:
                        return "densely dashed";
                    case DashStyle.Dotted:
                        return "dotted";
                    default:
                        return "densely dotted"; // Fallback style
                }
            }
        }

        #endregion

        #region Marker

        private string FormatTikzMarker
        {
            get
            {
                if (MarkerStyle == MarkerStyle.None)
                    return "";

                return $"mark={FormatTikzMarkerStyle},mark size={MarkerSize},mark options={{fill={Globals.Colors.GetColorName(MarkerColor)}}}";
            }
        }

        private string FormatTikzMarkerStyle
        {
            get
            {
                switch (MarkerStyle)
                {
                    case MarkerStyle.Dot:
                        return "*";
                    case MarkerStyle.Circle:
                        return "o";
                    case MarkerStyle.Diamond:
                        return "diamond*";
                    case MarkerStyle.Square:
                        return "square*";
                    case MarkerStyle.Rectangle:
                        return "square*";
                    case MarkerStyle.TriangleUp:
                        return "triangle*";
                    case MarkerStyle.TriangleDown:
                        return "triangle*";
                    case MarkerStyle.TriangleLeft:
                        return "pentagon*";
                    case MarkerStyle.TriangleRight:
                        return "pentagon*";
                    case MarkerStyle.Plus:
                        return "oplus*"; //return "+";
                    case MarkerStyle.Cross:
                        return "otimes*"; //return "x";
                    case MarkerStyle.None:
                        return "";
                    default:
                        return "plus";
                }
            }
        }

        #endregion

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
