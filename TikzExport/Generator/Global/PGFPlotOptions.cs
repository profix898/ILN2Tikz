using System;
using System.Collections.Generic;
using System.Drawing;
using ILNumerics.Drawing;
using static TikzExport.Generator.TikzColormapUtility;

namespace TikzExport.Generator.Global;

public sealed class PGFPlotOptions : List<string>, ITikzElement
{
    private readonly TikzGlobals _globals;

    public PGFPlotOptions(TikzGlobals globals)
    {
        _globals = globals;

        Add("compat=1.13"); // Minimum version
        Add("set layers"); // Sort layers
        Add("major grid style={solid,very thin,white!80!black}"); // Default major grid style
        Add("minor grid style={dashed,very thin,white!90!black}"); // Default minor grid style
        Add("minor grid style={dashed,very thin,white!90!black}"); // Default minor grid style
    }

    public void SetCompatiblity(string version)
    {
        // Replace compat version
        this[0] = "compat=" + version;
    }

    public void SetMajorGridStyle(Color gridColor, DashStyle gridStyle, int gridWidth)
    {
        // Replace default major grid style
        this[2] = $"major grid style={{{TikzFormatUtility.FormatLine(_globals, gridColor, gridStyle, 0.5f * gridWidth)}}}";
    }

    public void SetMinorGridStyle(Color gridColor, DashStyle gridStyle, int gridWidth)
    {
        // Replace default minor grid style
        this[3] = $"minor grid style={{{TikzFormatUtility.FormatLine(_globals, gridColor, gridStyle, 0.5f * gridWidth)}}}";
    }

    public void AddColormap(Colormap colormap)
    {
        // Global colormap
        Add(FormatColormap(colormap));
    }

    #region Implementation of ITikzElement

    public string PreTag
    {
        get { return ""; }
    }

    public IEnumerable<string> Content
    {
        get
        {
            // PGFPlots Options
            foreach (var option in this)
            {
                if (string.IsNullOrEmpty(option)) // Skip empty options
                    continue;

                yield return $"\\pgfplotsset{{{option}}}";
            }
        }
    }

    public string PostTag
    {
        get { return ""; }
    }

    public void Bind(Node node, TikzGlobals globals)
    {
        // NOTE: Don't bind automatically
        throw new NotSupportedException();
    }

    #endregion
}