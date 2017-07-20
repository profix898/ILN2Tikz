using System;
using System.Collections.Generic;
using System.IO;
using ILN2Tikz.Generator;
using ILN2Tikz.Generator.Plots;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;

namespace ILN2Tikz
{
    public static class ILN2Tikz
    {
        public static string ExportString(ILScene scene)
        {
            using (var stringWriter = new StringWriter())
            {
                Export(scene, stringWriter);

                return stringWriter.ToString();
            }
        }

        public static void ExportFile(ILScene scene, string filePath)
        {
            using (var streamWriter = new StreamWriter(filePath))
            {
                Export(scene, streamWriter);

                streamWriter.Flush();
            }
        }

        public static void Export(ILScene scene, TextWriter writer)
        {
            // Bind elements (maps ILNumerics to TikzElements)
            IList<ITikzElement> elements = new List<ITikzElement>();
            Bind(scene, elements);

            // Write to TextWriter
            var tikzWriter = new TikzWriter(writer);
            tikzWriter.Write(elements);
        }

        public static void Bind(ILScene scene, IList<ITikzElement> elements)
        {
            Bind<ILPlotCube, TikzAxis>(scene, elements);
            //Bind<ILLinePlot, TikzPlot>(scene, elements);
            //Bind<ILSurface, TikzPlot3>(scene, elements);
        }

        public static void Bind<TNode, TTikz>(ILGroup group, IList<ITikzElement> elements, object tag = null, Predicate<TNode> predicate = null)
            where TNode : ILNode
            where TTikz : ITikzElement, IRootBinder, new()
        {
            var element = group.First(tag, predicate);
            if (element == null)
                return;

            var tikz = new TTikz();
            tikz.Bind(element);

            elements.Add(tikz);
        }
    }
}