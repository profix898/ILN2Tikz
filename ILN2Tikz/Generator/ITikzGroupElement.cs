using System.Collections.Generic;
using ILN2Tikz.Generator.Global;
using ILNumerics.Drawing;

namespace ILN2Tikz.Generator;

public interface ITikzGroupElement : ITikzElement, ICollection<ITikzElement>
{
    void Bind(Group group, TikzGlobals globals);
}