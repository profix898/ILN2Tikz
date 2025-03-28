using Size = System.Drawing.Size;

namespace TikzExport.Generator.Global;

public sealed class TikzGlobals
{
    public TikzGlobals()
    {
        CanvasSize = new Size(100, 100);
        PGFPlotOptions = new PGFPlotOptions(this);
        Colors = new TikzColors();
    }

    public Size CanvasSize { get; set; }
        
    public PGFPlotOptions PGFPlotOptions { get; }

    public TikzColors Colors { get; }
}