using System;
using System.IO;
using ILN2Tikz.Generator;
using ILN2Tikz.Generator.Elements;
using ILN2Tikz.Generator.Global;
using ILNumerics.Drawing;
using Size = System.Drawing.Size;

namespace ILN2Tikz
{
    public static class ILN2TikzExport
    {
        public static string ExportString(Scene scene, Size canvasSize = default(Size))
        {
            if (scene == null)
                throw new ArgumentNullException(nameof(scene));

            using (var stringWriter = new StringWriter())
            {
                Export(scene, stringWriter, canvasSize);

                return stringWriter.ToString();
            }
        }

        public static void ExportFile(Scene scene, string filePath, Size canvasSize = default(Size))
        {
            if (scene == null)
                throw new ArgumentNullException(nameof(scene));
            if (String.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));

            using (var streamWriter = new StreamWriter(filePath))
            {
                Export(scene, streamWriter, canvasSize);

                streamWriter.Flush();
            }
        }

        public static void Export(Scene scene, TextWriter writer, Size canvasSize = default(Size))
        {
            if (scene == null)
                throw new ArgumentNullException(nameof(scene));
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            // Obtain TIKZ picture from Scene
            var tikzPicture = Bind(scene, canvasSize);

            // Write to TextWriter
            var tikzWriter = new TikzWriter(writer);
            tikzWriter.Write(tikzPicture);
        }

        public static TikzPicture Bind(Scene scene, Size canvasSize = default(Size))
        {
            if (scene == null)
                throw new ArgumentNullException(nameof(scene));

            scene.Configure();

            var tikzPicture = new TikzPicture(canvasSize);
            tikzPicture.Bind(scene, new TikzGlobals());

            return tikzPicture;
        }
    }
}