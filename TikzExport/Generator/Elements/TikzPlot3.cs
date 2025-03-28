using System;
using System.Collections.Generic;
using System.Diagnostics;
using ILNumerics.Drawing;
using TikzExport.Generator.Global;

namespace TikzExport.Generator.Elements;

public class TikzPlot3 : ITikzElement
{
    private Group surface;

    #region Implementation of ITikzElement

    public string PreTag
    {
        get { return @"\addplot3[surf,z buffer=sort,shader=faceted]"; }
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

    public void Bind(Node node, TikzGlobals globals)
    {
        if (node is FastSurface fastSurface)
        {
            surface = fastSurface;
            globals.PGFPlotOptions.AddColormap(fastSurface.Colormap);
        }
        else if (node is Surface surface)
        {
            this.surface = surface;
            globals.PGFPlotOptions.AddColormap(surface.Colormap);
        }
    }

    #endregion

    #region FormatHelper

    private static IEnumerable<string> DataTable(Group group)
    {
        var scaleModes = group.FirstUp<PlotCubeDataGroup>().ScaleModes;

        yield return "  table[row sep=crcr]{";

        if (group is Surface surface)
        {
            Array<float> positions = surface.Positions; // n x n x 3 (z, x, y)
            for (var i = 0; i < positions.S[0]; i++)
            {
                for (var j = 0; j < positions.S[1]; j++)
                {
                    var x = positions.GetValue(i, j, 1);
                    if (scaleModes.XAxisScale == AxisScale.Logarithmic)
                        x = (float) Math.Pow(10.0, x);
                    var y = positions.GetValue(i, j, 2);
                    if (scaleModes.YAxisScale == AxisScale.Logarithmic)
                        y = (float) Math.Pow(10.0, y);
                    var z = positions.GetValue(i, j, 0);
                    if (scaleModes.ZAxisScale == AxisScale.Logarithmic)
                        z = (float) Math.Pow(10.0, z);

                    yield return FormattableString.Invariant($"  {x}  {y} {z}\\\\");
                }
            }
        }
        else if (group is FastSurface fastSurface)
        {
            Array<float> positions = fastSurface.Fill.Positions.Storage; // 3 (z, x, y) x n
            Debug.Assert(positions.S[0] == 3);
            for (var i = 0; i < positions.S[1]; i++)
            {
                var x = positions.GetValue(0, i);
                if (scaleModes.XAxisScale == AxisScale.Logarithmic)
                    x = (float) Math.Pow(10.0, x);
                var y = positions.GetValue(1, i);
                if (scaleModes.YAxisScale == AxisScale.Logarithmic)
                    y = (float) Math.Pow(10.0, y);
                var z = positions.GetValue(2, i);
                if (scaleModes.ZAxisScale == AxisScale.Logarithmic)
                    z = (float) Math.Pow(10.0, z);

                if (!(Single.IsNaN(x) || Single.IsNaN(y) || Single.IsNaN(z)))
                    yield return FormattableString.Invariant($"  {x}  {y} {z}\\\\");
            }
        }

        yield return "};";
    }

    #endregion
}