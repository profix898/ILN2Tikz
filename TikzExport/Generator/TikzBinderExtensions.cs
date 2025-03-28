using System.Linq;
using ILNumerics.Drawing;
using TikzExport.Generator.Elements;
using TikzExport.Generator.Global;

namespace TikzExport.Generator;

public static class TikzBinderExtensions
{
    #region Generic

    public static void BindElement<TTikz>(this ITikzGroupElement tikzGroup, Node node, TikzGlobals globals)
        where TTikz : ITikzElement, new()
    {
        if (node == null)
            return;

        var tikz = new TTikz();
        tikz.Bind(node, globals);

        tikzGroup.Add(tikz);
    }

    public static void BindGroup<TTikzGroup>(this ITikzGroupElement tikzGroup, Group group, TikzGlobals globals)
        where TTikzGroup : ITikzGroupElement, new()
    {
        if (group == null)
            return;

        var tikz = new TTikzGroup();
        tikz.Bind(group, globals);

        tikzGroup.Add(tikz);
    }

    #endregion

    #region Plots

    public static void BindPlots(this ITikzGroupElement tikzGroup, Group group, TikzGlobals globals)
    {
        // ErrorBarPlot
        var errorBarPlots = group.Find<ErrorBarPlot>().ToArray();
        foreach (var errorBarPlot in errorBarPlots)
            tikzGroup.BindElement<TikzPlotPlus>(errorBarPlot, globals);

        // LinePlot
        var linePlots = group.Find<LinePlot>().ToArray();
        linePlots = linePlots.Where(lp => lp.Parent is not ErrorBarPlot).ToArray(); // Exclude LinePlots which are a direct child of ErrorBarPlot
        foreach (var linePlot in linePlots)
            tikzGroup.BindElement<TikzPlot>(linePlot, globals);

        // Surface
        var surfaces = group.Find<Surface>().ToArray();
        foreach (var surface in surfaces)
            tikzGroup.BindElement<TikzPlot3>(surface, globals);

        // FastSurface
        var fastSurfaces = group.Find<FastSurface>().ToArray();
        foreach (var fastSurface in fastSurfaces)
            tikzGroup.BindElement<TikzPlot3>(fastSurface, globals);
    }

    #endregion
}