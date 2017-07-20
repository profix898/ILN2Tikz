namespace ILN2Tikz.Generator.Plots
{
    public class TikzPlot3 : TikzElementBase
    {
        #region Implementation of ITikzElement

        public override string PreTag
        {
            get { return @"\addplot3["; }
        }

        public override string PostTag
        {
            get { return "]"; }
        }

        #endregion
    }
}