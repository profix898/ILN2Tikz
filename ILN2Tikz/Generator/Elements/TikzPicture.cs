using System.Collections.Generic;
using System.Drawing;
using ILN2Tikz.Generator.Global;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;

namespace ILN2Tikz.Generator.Elements
{
    public class TikzPicture : TikzGroupElementBase
    {
        private Globals globals;

        public TikzPicture(Size size = default(Size))
        {
            if (size.Width == 0 || size.Height == 0)
                size = new Size(100, 100);

            Size = size;
        }

        public Size Size { get; }

        #region Implementation of ITikzElement

        public override string PreTag
        {
            get { return "% Created via ILN2TIKZ converter"; }
        }

        public override IEnumerable<string> Content
        {
            get
            {
                // Colors
                foreach (var colorDef in globals.Colors.Content)
                    yield return colorDef;

                yield return "";

                yield return @"\begin{tikzpicture}";

                // PGFPlots Options
                foreach (var pgfPlotsOption in globals.PGFPlotOptions.Content)
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

        public override void Bind(ILGroup group, Globals globals)
        {
            this.globals = globals;
            globals.Size = Size; // Set size of canvas

            var scene = group as ILScene;
            if (scene == null)
                return;

            // NOTE: Only ILPlotCube is supported for now
            this.BindGroup<TikzAxis>(scene.First<ILPlotCube>(), globals);
        }

        #endregion
    }
}
