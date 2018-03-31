using ILN2Tikz.Generator.Elements;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;

namespace ILN2Tikz.Generator
{
    public static class TikzBinderExtensions
    {
        #region Generic

        public static void BindElement<TTikz>(this ITikzGroupElement tikzGroup, ILNode node)
            where TTikz : ITikzElement, new()
        {
            if (node == null)
                return;

            var tikz = new TTikz();
            tikz.Bind(node);

            tikzGroup.Add(tikz);
        }

        public static void BindGroup<TTikzGroup>(this ITikzGroupElement tikzGroup, ILGroup group)
            where TTikzGroup : ITikzGroupElement, new()
        {
            if (group == null)
                return;

            var tikz = new TTikzGroup();
            tikz.Bind(group);

            tikzGroup.Add(tikz);
        }

        #endregion

        #region Plots

        public static void BindPlots(this ITikzGroupElement tikzGroup, ILGroup group)
        {
            // LinePlots
            foreach (var linePlot in group.Find<ILLinePlot>())
                tikzGroup.BindElement<TikzPlot>(linePlot);

            // SurfacePlots
            foreach (var surface in group.Find<ILSurface>())
                tikzGroup.BindElement<TikzPlot3>(surface);
        }

        #endregion
    }
}