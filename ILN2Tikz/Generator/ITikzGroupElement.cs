using System.Collections.Generic;
using ILNumerics.Drawing;

namespace ILN2Tikz.Generator
{
    public interface ITikzGroupElement : ITikzElement, ICollection<ITikzElement>
    {
        void Bind(ILGroup group);
    }
}