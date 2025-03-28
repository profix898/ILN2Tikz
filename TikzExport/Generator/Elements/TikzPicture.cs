using System.Collections.Generic;
using ILNumerics.Drawing;
using TikzExport.Generator.Global;
using Size = System.Drawing.Size;

namespace TikzExport.Generator.Elements;

public class TikzPicture : TikzGroupElementBase
{
    private TikzGlobals _globals;

    public TikzPicture(Size canvasSize = default)
    {
        if (canvasSize.Width == 0 || canvasSize.Height == 0)
            canvasSize = new Size(100, 100); // Default size

        CanvasSize = canvasSize;
    }

    public Size CanvasSize { get; }

    #region Implementation of ITikzElement

    public override string PreTag
    {
        get { return "% Created via TikzExport (by the ILNumerics Community)"; }
    }

    public override IEnumerable<string> Content
    {
        get
        {
            // Colors
            foreach (var colorDef in _globals.Colors.Content)
                yield return colorDef;

            yield return "";

            yield return @"\begin{tikzpicture}";

            // PGFPlots Options
            foreach (var pgfPlotsOption in _globals.PGFPlotOptions.Content)
                yield return pgfPlotsOption;

            // Child Elements
            foreach (var contentLine in base.Content)
                yield return contentLine;
        }
    }

    public override string PostTag
    {
        get { return @"\end{tikzpicture}"; }
    }

    public override void Bind(Group group, TikzGlobals globals)
    {
        _globals = globals;
        globals.CanvasSize = CanvasSize;

        var scene = group as Scene;
        if (scene == null)
            return;

        // NOTE: Only PlotCube is supported for now
        this.BindGroup<TikzAxis>(scene.First<PlotCube>(), globals);
    }

    #endregion
}