using System.Drawing;

namespace ILN2Tikz.Generator.Global
{
    public sealed class Globals
    {
        public Globals()
        {
            CanvasSize = new Size(100, 100);
            PGFPlotOptions = new PGFPlotOptions(this);
            Colors = new TikzColors();
        }

        public Size CanvasSize { get; set; }
        
        public PGFPlotOptions PGFPlotOptions { get; }

        public TikzColors Colors { get; }
    }
}