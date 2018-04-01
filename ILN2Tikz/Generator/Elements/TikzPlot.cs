using System;
using System.Collections.Generic;
using System.Drawing;
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
                    return $"\\addplot[{TikzLine}]";

                return $"\\addplot[{TikzLine},{TikzMarker}]";
            }
        }

        public IEnumerable<string> Content
        {
            get
            {
                if (linePlot != null)
                {
                    foreach (var tableEntry in DataTable(linePlot))
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
            if (!(node is ILLinePlot linePlot))
                return;

            this.linePlot = linePlot; // Reference for data table

            // Line
            LineColor = linePlot.Line.Color ?? Color.Black;
            if (!Globals.Colors.Contains(LineColor))
                Globals.Colors.Add(LineColor);
            LineWidth = linePlot.Line.Width;
            DashStyle = linePlot.Line.DashStyle;

            // Marker
            MarkerStyle = linePlot.Marker.Style;
            MarkerSize = linePlot.Marker.Size;
        }

        #endregion

        #region Line

        public Color LineColor { get; set; }

        public int LineWidth { get; set; }

        public DashStyle DashStyle { get; set; }

        #endregion

        #region Marker

        public MarkerStyle MarkerStyle { get; set; }

        public int MarkerSize { get; set; }

        #endregion

        #region FormatHelper

        #region Line

        private string TikzLine
        {
            get { return $"color={Globals.Colors.GetColorName(LineColor)},{TikzDashStyle},line width={LineWidth}pt"; }
        }

        private string TikzDashStyle
        {
            get
            {
                switch (DashStyle)
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

        private string TikzMarker
        {
            get
            {
                if (MarkerStyle == MarkerStyle.None)
                    return "";

                return $"mark={TikzMarkerStyle},mark size={MarkerSize}";
            }
        }

        private string TikzMarkerStyle
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
                        return "diamond";
                    case MarkerStyle.Square:
                        return "square";
                    case MarkerStyle.Rectangle:
                        return "square";
                    case MarkerStyle.TriangleUp:
                        return "triangle";
                    case MarkerStyle.TriangleDown:
                        return "triangle";
                    case MarkerStyle.TriangleLeft:
                        return "pentagon";
                    case MarkerStyle.TriangleRight:
                        return "pentagon";
                    case MarkerStyle.Plus:
                        return "+";
                    case MarkerStyle.Cross:
                        return "x";
                    case MarkerStyle.None:
                        return "";
                    default:
                        return "plus";
                }
            }
        }

        #endregion

        private static IEnumerable<string> DataTable(ILLinePlot linePlot)
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
