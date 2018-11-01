using ILN2Tikz.Generator.Elements;
using ILN2Tikz.Generator.Global;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;

namespace ILN2Tikz.Generator
{
    public static class TikzBinderExtensions
    {
        #region Generic

        public static void BindElement<TTikz>(this ITikzGroupElement tikzGroup, Node node, Globals globals)
            where TTikz : ITikzElement, new()
        {
            if (node == null)
                return;

            var tikz = new TTikz();
            tikz.Bind(node, globals);

            tikzGroup.Add(tikz);
        }

        public static void BindGroup<TTikzGroup>(this ITikzGroupElement tikzGroup, Group group, Globals globals)
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

        public static void BindPlots(this ITikzGroupElement tikzGroup, Group group, Globals globals)
        {
            // LinePlots
            foreach (var linePlot in group.Find<LinePlot>())
                tikzGroup.BindElement<TikzPlot>(linePlot, globals);

            // SurfacePlots
            foreach (var surface in group.Find<Surface>())
                tikzGroup.BindElement<TikzPlot3>(surface, globals);
        }

        #endregion
    }
}