using System.Collections.Generic;
using ILN2Tikz.Generator.Global;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;

namespace ILN2Tikz.Generator.Elements
{
    public class TikzPicture : TikzGroupElementBase
    {
        #region Implementation of ITikzElement

        public override string PreTag
        {
            get { return "% Created via ILNumerics to TIKZ converter"; }
        }

        public override IEnumerable<string> Content
        {
            get
            {
                // Colors
                foreach (var colorDef in Globals.Colors.Content)
                    yield return colorDef;

                yield return "";

                yield return @"\begin{tikzpicture}";

                // PGFPlots Options
                foreach (var pgfPlotsOption in Globals.PGFPlotOptions.Content)
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
