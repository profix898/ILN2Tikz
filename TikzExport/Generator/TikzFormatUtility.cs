﻿using System;
using System.Drawing;
using ILNumerics.Drawing;
using TikzExport.Generator.Global;

namespace TikzExport.Generator;

public static class TikzFormatUtility
{
    #region Line

    internal static string FormatLine(TikzGlobals globals, Color color, DashStyle style, float width)
    {
        return FormattableString.Invariant($"color={globals.Colors.GetColorName(color)},{FormatDashStyle(style)},line width={width:F1}pt");
    }

    internal static string FormatLine(TikzGlobals globals, Color color, DashStyle style, int width)
    {
        return FormattableString.Invariant($"color={globals.Colors.GetColorName(color)},{FormatDashStyle(style)},line width={width}pt");
    }

    internal static string FormatDashStyle(DashStyle style)
    {
        switch (style)
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

    #endregion

    #region Marker

    internal static string FormatMarker(TikzGlobals globals, Color color, MarkerStyle style, int size)
    {
        if (style == MarkerStyle.None)
            return "";

        return $"mark={FormatMarkerStyle(style)},mark size={size},mark options={{{globals.Colors.GetColorName(color)}}}";
    }

    internal static string FormatMarkerStyle(MarkerStyle style)
    {
        switch (style)
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
                return "+";
            case MarkerStyle.Cross:
                return "x";
            case MarkerStyle.None:
                return "";
            default:
                return "plus";
        }
    }

    #endregion

    #region ErrorBar

    internal static string FormatErrorBars(TikzGlobals globals, Color color, DashStyle style, int width)
    {
        var errorBarDef = "error bars/.cd, y fixed,y dir=both, y explicit";
        var errorBarStyle = FormatLine(globals, color, style, width);

        return $"{errorBarDef},error bar style={{{errorBarStyle}}}";
    }

    #endregion
}