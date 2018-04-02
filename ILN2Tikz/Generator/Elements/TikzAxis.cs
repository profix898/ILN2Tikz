using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using ILN2Tikz.Generator.Global;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;

namespace ILN2Tikz.Generator.Elements
{
    public class TikzAxis : TikzGroupElementBase, ITikzGroupElement
    {
        private float[] xTicks;
        private float[] yTicks;
        private float[] zTicks;

        public enum TicksModeEnum
        {
            None,
            Coordinate,
            Custom,
            Auto
        }

        public enum TicksAlignEnum
        {
            Inside,
            Center,
            Outside
        }

        #region Implementation of ITikzElement

        public override string PreTag
        {
            get { return @"\begin{axis}["; }
        }

        public override IEnumerable<string> Content
        {
            get
            {
                #region Global

                yield return $"  width={Size.Width}mm,";
                yield return $"  height={Size.Height}mm,";

                if (!String.IsNullOrEmpty(Title))
                    yield return $"  title='{{{Title}}},";

                yield return FormattableString.Invariant($"  view={{({ViewAzimuth})}}{{({ViewElevation})}},");

                #endregion

                #region XAxis

                if (!String.IsNullOrEmpty(XLabel))
                    yield return $"  xlabel={{{XLabel}}},";
                switch (XScale)
                {
                    case AxisScale.Linear:
                        yield return "  xmode=normal,";
                        break;
                    case AxisScale.Logarithmic:
                        yield return "  xmode=log,";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                yield return FormattableString.Invariant($"  xmin={XMin},");
                yield return FormattableString.Invariant($"  xmax={XMax},");
                switch (XTicksMode)
                {
                    case TicksModeEnum.None:
                        //yield return @"  xticks=\empty,";
                        break;
                    case TicksModeEnum.Coordinate:
                        yield return "  xticks=data,";
                        break;
                    case TicksModeEnum.Custom:
                        if (XTicks != null)
                            yield return $"  xticks={String.Join(",", XTicks.Select(x => x.ToString("F", CultureInfo.InvariantCulture)))}";
                        break;
                    case TicksModeEnum.Auto:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                switch (XTicksAlign)
                {
                    case TicksAlignEnum.Inside:
                        yield return "  xtick align=inside,";
                        break;
                    case TicksAlignEnum.Center:
                        yield return "  xtick align=center,";
                        break;
                    case TicksAlignEnum.Outside:
                        yield return "  xtick align=outside,";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                yield return $"  xmajorticks={(XMajorTicks ? "true" : "false")},";
                yield return $"  xminorticks={(XMinorTicks ? "true" : "false")},";
                if (XMajorGrid)
                    yield return "  xmajorgrids,";
                if (XMinorGrid)
                    yield return "  xminorgrids,";

                #endregion

                #region YAxis

                if (!String.IsNullOrEmpty(YLabel))
                    yield return $"  ylabel={{{YLabel}}},";
                switch (YScale)
                {
                    case AxisScale.Linear:
                        yield return "  ymode=normal,";
                        break;
                    case AxisScale.Logarithmic:
                        yield return "  ymode=log,";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                yield return FormattableString.Invariant($"  ymin={YMin},");
                yield return FormattableString.Invariant($"  ymax={YMax},");
                switch (YTicksMode)
                {
                    case TicksModeEnum.None:
                        //yield return @"  yticks=\empty,";
                        break;
                    case TicksModeEnum.Coordinate:
                        yield return "  yticks=data,";
                        break;
                    case TicksModeEnum.Custom:
                        if (YTicks != null)
                            yield return $"  yticks={String.Join(",", YTicks.Select(x => x.ToString("F", CultureInfo.InvariantCulture)))}";
                        break;
                    case TicksModeEnum.Auto:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                switch (YTicksAlign)
                {
                    case TicksAlignEnum.Inside:
                        yield return "  ytick align=inside,";
                        break;
                    case TicksAlignEnum.Center:
                        yield return "  ytick align=center,";
                        break;
                    case TicksAlignEnum.Outside:
                        yield return "  ytick align=outside,";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                yield return $"  ymajorticks={(YMajorTicks ? "true" : "false")},";
                yield return $"  yminorticks={(YMinorTicks ? "true" : "false")},";
                if (YMajorGrid)
                    yield return "  ymajorgrids,";
                if (YMinorGrid)
                    yield return "  yminorgrids,";

                #endregion

                #region ZAxis

                if (!TwoDMode)
                {
                    if (!String.IsNullOrEmpty(ZLabel))
                        yield return $"  zlabel={{{ZLabel}}},";
                    switch (ZScale)
                    {
                        case AxisScale.Linear:
                            yield return "  zmode=normal,";
                            break;
                        case AxisScale.Logarithmic:
                            yield return "  zmode=log,";
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    yield return FormattableString.Invariant($"  zmin={ZMin},");
                    yield return FormattableString.Invariant($"  zmax={ZMax},");
                    switch (ZTicksMode)
                    {
                        case TicksModeEnum.None:
                            //yield return @"  zticks=\empty,";
                            break;
                        case TicksModeEnum.Coordinate:
                            yield return "  zticks=data,";
                            break;
                        case TicksModeEnum.Custom:
                            if (ZTicks != null)
                                yield return $"  zticks={String.Join(",", ZTicks.Select(x => x.ToString("F", CultureInfo.InvariantCulture)))}";
                            break;
                        case TicksModeEnum.Auto:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    switch (ZTicksAlign)
                    {
                        case TicksAlignEnum.Inside:
                            yield return "  ztick align=inside,";
                            break;
                        case TicksAlignEnum.Center:
                            yield return "  ztick align=center,";
                            break;
                        case TicksAlignEnum.Outside:
                            yield return "  ztick align=outside,";
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    yield return $"  zmajorticks={(ZMajorTicks ? "true" : "false")},";
                    yield return $"  zminorticks={(ZMinorTicks ? "true" : "false")},";
                    if (ZMajorGrid)
                        yield return "  zmajorgrids,";
                    if (ZMinorGrid)
                        yield return "  zminorgrids,";
                }

                #endregion

                #region Legend

                if (LegendVisible)
                    yield return FormatTikzLegendStyle;

                #endregion

                yield return "]";

                // Return child elements
                foreach (var element in base.Content)
                    yield return element;
            }
        }

        public override string PostTag
        {
            get { return @"\end{axis}"; }
        }

        #endregion

        #region Implementation of ITikzGroupElement

        public override void Bind(ILGroup group)
        {
            var plotCube = group as ILPlotCube;
            if (plotCube == null)
                return;

            // Global
            var title = group.First<ILTitle>();
            Title = title?.Label?.Text ?? String.Empty;
            Size = new Rectangle(0, 0, 150, 100); // TODO: Aspect ratio
            TwoDMode = plotCube.TwoDMode;
            if (!TwoDMode)
            {
                // Default orientation for 3D view
                ViewAzimuth = 60;
                ViewElevation = 60;
            }

            // XAxis
            XLabel = plotCube.Axes.XAxis.Label.Text;
            XScale = plotCube.ScaleModes.XAxisScale;
            XMin = plotCube.Axes.XAxis.Min ?? plotCube.Plots.Limits.XMin;
            XMax = plotCube.Axes.XAxis.Max ?? plotCube.Plots.Limits.XMax;
            if (plotCube.ScaleModes.XAxisScale == AxisScale.Logarithmic)
            {
                XMin = (float) Math.Pow(10.0, XMin);
                XMax = (float) Math.Pow(10.0, XMax);
            }
            XTicksAlign = TickLengthToTicksAlignEnum(plotCube.Axes.XAxis.Ticks.TickLength);
            XMajorTicks = plotCube.Axes.XAxis.Ticks.Visible;
            XMinorTicks = false;
            XMajorGrid = plotCube.Axes.XAxis.GridMajor.Visible;
            XMinorGrid = plotCube.Axes.XAxis.GridMinor.Visible;
            
            // YAxis
            YLabel = plotCube.Axes.YAxis.Label.Text;
            YScale = plotCube.ScaleModes.YAxisScale;
            YMin = plotCube.Axes.YAxis.Min ?? plotCube.Plots.Limits.YMin;
            YMax = plotCube.Axes.YAxis.Max ?? plotCube.Plots.Limits.YMax;
            if (plotCube.ScaleModes.YAxisScale == AxisScale.Logarithmic)
            {
                YMin = (float) Math.Pow(10.0, YMin);
                YMax = (float) Math.Pow(10.0, YMax);
            }
            YTicksAlign = TickLengthToTicksAlignEnum(plotCube.Axes.YAxis.Ticks.TickLength);
            YMajorTicks = plotCube.Axes.YAxis.Ticks.Visible;
            YMinorTicks = false;
            YMajorGrid = plotCube.Axes.YAxis.GridMajor.Visible;
            YMinorGrid = plotCube.Axes.YAxis.GridMinor.Visible;
            
            // ZAxis
            ZLabel = plotCube.Axes.ZAxis.Label.Text;
            ZScale = plotCube.ScaleModes.ZAxisScale;
            ZMin = plotCube.Axes.ZAxis.Min ?? plotCube.Plots.Limits.ZMin;
            ZMax = plotCube.Axes.ZAxis.Max ?? plotCube.Plots.Limits.ZMax;
            if (plotCube.ScaleModes.ZAxisScale == AxisScale.Logarithmic)
            {
                ZMin = (float) Math.Pow(10.0, ZMin);
                ZMax = (float) Math.Pow(10.0, ZMax);
            }
            ZTicksAlign = TickLengthToTicksAlignEnum(plotCube.Axes.ZAxis.Ticks.TickLength);
            ZMajorTicks = plotCube.Axes.ZAxis.Ticks.Visible;
            ZMinorTicks = false;
            ZMajorGrid = plotCube.Axes.ZAxis.GridMajor.Visible;
            ZMinorGrid = plotCube.Axes.ZAxis.GridMinor.Visible;

            // Legend
            var legend = plotCube.First<ILLegend>();
            if (legend != null)
            {
                LegendVisible = legend.Visible;
                LegendLocation = legend.Location;
                LegendBorderColor = legend.Border.Color ?? Color.Black;
                Globals.Colors.Add(LegendBorderColor);
                LegendBackgroundColor = legend.Background.Color ?? Color.White;
                Globals.Colors.Add(LegendBackgroundColor);
            }
            
            // Map plots (ILLinePlot, ILSurface, etc.)
            this.BindPlots(plotCube);
        }

        #endregion

        #region Properties

        #region Global

        public string Title { get; set; }

        public float ViewAzimuth { get; set; }

        public float ViewElevation { get; set; }

        public Rectangle Size { get; set; }

        public bool TwoDMode { get; set; }

        public TicksAlignEnum TicksAlign
        {
            set { XTicksAlign = YTicksAlign = ZTicksAlign = value; }
        }

        public bool MajorTicks
        {
            set { XMajorTicks = YMajorTicks = ZMajorTicks = value; }
        }

        public bool MinorTicks
        {
            set { XMinorTicks = YMinorTicks = ZMinorTicks = value; }
        }

        public bool Grid
        {
            set { XMajorGrid = MinorGrid = value; }
        }

        public bool MajorGrid
        {
            set { XMajorGrid = YMajorGrid = ZMajorGrid = value; }
        }

        public bool MinorGrid
        {
            set { XMinorGrid = YMinorGrid = ZMinorGrid = value; }
        }

        #endregion

        #region XAxis

        public string XLabel { get; set; }

        public AxisScale XScale { get; set; }

        public float XMin { get; set; }

        public float XMax { get; set; }

        public TicksModeEnum XTicksMode { get; set; }

        public float[] XTicks
        {
            get { return xTicks; }
            set
            {
                xTicks = value;
                XTicksMode = TicksModeEnum.Coordinate;
            }
        }

        public TicksAlignEnum XTicksAlign { get; set; }

        public bool XMajorTicks { get; set; }

        public bool XMinorTicks { get; set; }

        public bool XMajorGrid { get; set; }

        public bool XMinorGrid { get; set; }

        #endregion

        #region YAxis

        public string YLabel { get; set; }

        public AxisScale YScale { get; set; }

        public float YMin { get; set; }

        public float YMax { get; set; }

        public TicksModeEnum YTicksMode { get; set; }

        public float[] YTicks
        {
            get { return yTicks; }
            set
            {
                yTicks = value;
                YTicksMode = TicksModeEnum.Coordinate;
            }
        }

        public TicksAlignEnum YTicksAlign { get; set; }

        public bool YMajorTicks { get; set; }

        public bool YMinorTicks { get; set; }

        public bool YMajorGrid { get; set; }

        public bool YMinorGrid { get; set; }

        #endregion

        #region ZAxis

        public string ZLabel { get; set; }

        public AxisScale ZScale { get; set; }

        public float ZMin { get; set; }

        public float ZMax { get; set; }

        public TicksModeEnum ZTicksMode { get; set; }

        public float[] ZTicks
        {
            get { return zTicks; }
            set
            {
                zTicks = value;
                ZTicksMode = TicksModeEnum.Coordinate;
            }
        }

        public TicksAlignEnum ZTicksAlign { get; set; }

        public bool ZMajorTicks { get; set; }

        public bool ZMinorTicks { get; set; }

        public bool ZMajorGrid { get; set; }

        public bool ZMinorGrid { get; set; }

        #endregion

        #region Legend

        public bool LegendVisible { get; set; }

        public PointF LegendLocation { get; set; }

        public Color LegendBorderColor { get; set; }

        public Color LegendBackgroundColor { get; set; }

        #endregion

        #endregion

        #region Helpers

        #region Legend

        private string FormatTikzLegendStyle
        {
            get
            {
                return FormattableString.Invariant($"  legend style={{legend cell align=left,align=left,{FormatTikzLegendColors},{FormatTikzLegendLocation}}},");
            }
        }

        private string FormatTikzLegendColors
        {
            get
            {
                return FormattableString.Invariant($"fill={Globals.Colors.GetColorName(LegendBackgroundColor)},draw={Globals.Colors.GetColorName(LegendBorderColor)}");
            }
        }

        private string FormatTikzLegendLocation
        {
            get
            {
                return FormattableString.Invariant($"at={{({LegendLocation.X},{1f-LegendLocation.Y})}}");
            }
        }

        #endregion

        private TicksAlignEnum TickLengthToTicksAlignEnum(float tickLength)
        {
            if (tickLength < 0)
                return TicksAlignEnum.Inside;

            if (tickLength > 0)
                return TicksAlignEnum.Outside;

            return TicksAlignEnum.Center;
        }

        #endregion
    }
}
