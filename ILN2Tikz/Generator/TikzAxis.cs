using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ILNumerics.Drawing;

namespace ILN2Tikz.Generator
{
    public class TikzAxis : TikzElementBase, IRootBinder
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

        public enum AxisScaleEnum
        {
            Linear,
            Logarithmic
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

                yield return $"width={Size.Width}cm,";
                yield return $"height={Size.Height}cm,";

                if (!String.IsNullOrEmpty(Title))
                    yield return $"title={Title},";

                yield return $"view={{({ViewAzimuth})}}{{({ViewElevation})}},";

                #endregion

                #region XAxis

                if (!String.IsNullOrEmpty(XLabel))
                    yield return $"xlabel={XLabel},";
                switch (XScale)
                {
                    case AxisScaleEnum.Linear:
                        yield return "xmode=normal,";
                        break;
                    case AxisScaleEnum.Logarithmic:
                        yield return "xmode=log,";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                yield return $"xmin={XMin},";
                yield return $"xmax={XMax},";
                switch (XTicksMode)
                {
                    case TicksModeEnum.None:
                        yield return @"xticks=\empty,";
                        break;
                    case TicksModeEnum.Coordinate:
                        yield return "xticks=data,";
                        break;
                    case TicksModeEnum.Custom:
                        if (XTicks != null)
                            yield return $"xticks={String.Join(",", XTicks.Select(x => x.ToString("F")))}";
                        break;
                    case TicksModeEnum.Auto:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                switch (XTicksAlign)
                {
                    case TicksAlignEnum.Inside:
                        yield return "xtick align=inside,";
                        break;
                    case TicksAlignEnum.Center:
                        yield return "xtick align=center,";
                        break;
                    case TicksAlignEnum.Outside:
                        yield return "xtick align=outside,";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                yield return $"xmajorticks={(XMajorTicks ? "true" : "false")},";
                yield return $"xminorticks={(XMinorTicks ? "true" : "false")},";
                if (XMajorGrid)
                    yield return "xmajorgrids,";
                if (XMinorGrid)
                    yield return "xminorgrids,";

                #endregion

                #region YAxis

                if (!String.IsNullOrEmpty(YLabel))
                    yield return $"ylabel={YLabel},";
                switch (YScale)
                {
                    case AxisScaleEnum.Linear:
                        yield return "ymode=normal,";
                        break;
                    case AxisScaleEnum.Logarithmic:
                        yield return "ymode=log,";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                yield return $"ymin={YMin},";
                yield return $"ymax={YMax},";
                switch (YTicksMode)
                {
                    case TicksModeEnum.None:
                        yield return @"yticks=\empty,";
                        break;
                    case TicksModeEnum.Coordinate:
                        yield return "yticks=data,";
                        break;
                    case TicksModeEnum.Custom:
                        if (YTicks != null)
                            yield return $"yticks={String.Join(",", YTicks.Select(x => x.ToString("F")))}";
                        break;
                    case TicksModeEnum.Auto:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                switch (YTicksAlign)
                {
                    case TicksAlignEnum.Inside:
                        yield return "ytick align=inside,";
                        break;
                    case TicksAlignEnum.Center:
                        yield return "ytick align=center,";
                        break;
                    case TicksAlignEnum.Outside:
                        yield return "ytick align=outside,";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                yield return $"ymajorticks={(YMajorTicks ? "true" : "false")},";
                yield return $"yminorticks={(YMinorTicks ? "true" : "false")},";
                if (YMajorGrid)
                    yield return "ymajorgrids,";
                if (YMinorGrid)
                    yield return "yminorgrids,";

                #endregion

                #region ZAxis

                if (!String.IsNullOrEmpty(ZLabel))
                    yield return $"xlabel={ZLabel},";
                switch (ZScale)
                {
                    case AxisScaleEnum.Linear:
                        yield return "zmode=normal,";
                        break;
                    case AxisScaleEnum.Logarithmic:
                        yield return "zmode=log,";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                yield return $"zmin={ZMin},";
                yield return $"zmax={ZMax},";
                switch (ZTicksMode)
                {
                    case TicksModeEnum.None:
                        yield return @"zticks=\empty,";
                        break;
                    case TicksModeEnum.Coordinate:
                        yield return "zticks=data,";
                        break;
                    case TicksModeEnum.Custom:
                        if (ZTicks != null)
                            yield return $"zticks={String.Join(",", ZTicks.Select(x => x.ToString("F")))}";
                        break;
                    case TicksModeEnum.Auto:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                switch (ZTicksAlign)
                {
                    case TicksAlignEnum.Inside:
                        yield return "ztick align=inside,";
                        break;
                    case TicksAlignEnum.Center:
                        yield return "ztick align=center,";
                        break;
                    case TicksAlignEnum.Outside:
                        yield return "ztick align=outside,";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                yield return $"zmajorticks={(ZMajorTicks ? "true" : "false")},";
                yield return $"zminorticks={(ZMinorTicks ? "true" : "false")},";
                if (ZMajorGrid)
                    yield return "zmajorgrids,";
                if (ZMinorGrid)
                    yield return "zminorgrids,";

                #endregion

                // Return child elements
                foreach (var element in base.Content)
                    yield return element;
            }
        }

        public override string PostTag
        {
            get { return @"]"; }
        }

        #endregion

        #region Global

        public string Title { get; set; }

        public float ViewAzimuth { get; set; }

        public float ViewElevation { get; set; }

        public Rectangle Size { get; set; }

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

        public AxisScaleEnum XScale { get; set; }

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

        public AxisScaleEnum YScale { get; set; }

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

        public AxisScaleEnum ZScale { get; set; }

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

        /// <summary>
        /// Binds the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        public void Bind(ILNode node)
        {
            // TODO: Find ILPlotCube properties and map to Tikz axis properties
            // TODO: Map children
        }
    }
}
