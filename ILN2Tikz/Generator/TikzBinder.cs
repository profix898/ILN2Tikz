using System.Collections.Generic;
using ILNumerics.Drawing;

namespace ILN2Tikz.Generator
{
    public static class TikzBinder
    {
        public static void BindElement<TTikz>(this ICollection<ITikzElement> elements, ILNode node)
            where TTikz : ITikzElement, new()
        {
            if (node == null)
                return;

            var tikz = new TTikz();
            tikz.Bind(node);

            elements.Add(tikz);
        }

        public static void BindGroup<TTikzGroup>(this ICollection<ITikzElement> elements, ILGroup group)
            where TTikzGroup : ITikzGroupElement, new()
        {
            if (group == null)
                return;

            var tikz = new TTikzGroup();
            tikz.Bind(group);

            elements.Add(tikz);
        }
    }
}