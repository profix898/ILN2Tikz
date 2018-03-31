using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;

namespace ILN2Tikz.Generator
{
    public class TikzPicture : TikzGroupElementBase
    {
        #region Implementation of ITikzElement

        public override string PreTag
        {
            // TODO: \pgfplotsset{set layers} -> pgfplots options
            get { return @"\begin{tikzpicture}"; }
        }

        public override string PostTag
        {
            get { return @"\end{tikzpicture}"; }
        }

        public override void Bind(ILGroup group)
        {
            var scene = group as ILScene;
            if (scene == null)
                return;

            // NOTE: Only ILPlotCube is supported for now
            this.BindGroup<TikzAxis>(scene.First<ILPlotCube>());
        }

        #endregion
    }
}
