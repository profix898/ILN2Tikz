using System;
using System.Collections.Generic;
using ILNumerics;
using ILNumerics.Drawing.Plotting;

namespace ILN2Tikz.Generator
{
    internal static class TableHelper
    {
        public static IEnumerable<string> TikzTable(ILLinePlot linePlot)
        {
            yield return "  table[row sep=crcr]{";

            ILArray<float> positions = linePlot.Positions; // 3 x n
            for (var i = 0; i < positions.S[1]; i++)
            {
                ILArray<float> xyz = positions[ILMath.full, i];
                var x = (float) xyz[0];
                var y = (float) xyz[1];

                yield return FormattableString.Invariant($"  {x}	{y}\\\\");
            }

            yield return "};";
        }

        public static IEnumerable<string> TikzTable(ILSurface surface)
        {
            yield return "  table[row sep=crcr]{";

            ILArray<float> positions = surface.Positions; // 3 x n
            for (var i = 0; i < positions.S[1]; i++)
            {
                ILArray<float> xyz = positions[ILMath.full, i];
                var x = (float) xyz[0];
                var y = (float) xyz[1];
                var z = (float) xyz[2];

                yield return FormattableString.Invariant($"  {x}  {y} {z}\\\\");
            }

            yield return "};";
        }
    }
}