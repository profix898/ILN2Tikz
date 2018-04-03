using System;
using System.Drawing;
using System.IO;
using ILN2Tikz.Generator;
using ILN2Tikz.Generator.Elements;
using ILN2Tikz.Generator.Global;
using ILNumerics.Drawing;

namespace ILN2Tikz
{
    public static class ILN2TikzExport
    {
        public static string ExportString(ILScene scene, Size size = default(Size))
        {
            if (scene == null)
                throw new ArgumentNullException(nameof(scene));

            using (var stringWriter = new StringWriter())
            {
                Export(scene, stringWriter, size);

                return stringWriter.ToString();
            }
        }

        public static void ExportFile(ILScene scene, string filePath, Size size = default(Size))
        {
            if (scene == null)
                throw new ArgumentNullException(nameof(scene));
            if (String.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));

            using (var streamWriter = new StreamWriter(filePath))
            {
                Export(scene, streamWriter, size);

                streamWriter.Flush();
            }
        }

        public static void Export(ILScene scene, TextWriter writer, Size size = default(Size))
        {
            if (scene == null)
                throw new ArgumentNullException(nameof(scene));
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            // Obtain TIKZ picture from ILScene
            var tikzPicture = Bind(scene, size);

            // Write to TextWriter
            var tikzWriter = new TikzWriter(writer);
            tikzWriter.Write(tikzPicture);
        }

        public static TikzPicture Bind(ILScene scene, Size size = default(Size))
        {
            if (scene == null)
                throw new ArgumentNullException(nameof(scene));

            var tikzPicture = new TikzPicture(size);
            tikzPicture.Bind(scene, new Globals());

            return tikzPicture;
        }
    }
}