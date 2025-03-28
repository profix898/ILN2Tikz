using System.Collections.Generic;
using ILNumerics.Drawing;
using TikzExport.Generator.Global;

namespace TikzExport.Generator;

public interface ITikzGroupElement : ITikzElement, ICollection<ITikzElement>
{
    void Bind(Group group, TikzGlobals globals);
}