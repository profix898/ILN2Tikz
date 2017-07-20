namespace ILN2Tikz.Generator
{
    public class TikzPicture : TikzElementBase
    {
        #region Implementation of ITikzElement

        public override string PreTag
        {
            get { return @"\begin{tikzpicture}"; }
        }

        public override string PostTag
        {
            get { return @"\end{tikzpicture}"; }
        }

        #endregion
    }
}
