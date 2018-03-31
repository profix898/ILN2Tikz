using System.IO;
using ILN2Tikz.Generator;
using ILNumerics.Drawing;

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
            // Obtain TIKZ picture from ILScene
            var tikzPicture = Bind(scene);

            // Write to TextWriter
            var tikzWriter = new TikzWriter(writer);
            tikzWriter.Write(tikzPicture);
        }

        public static TikzPicture Bind(ILScene scene)
        {
            var tikzPicture = new TikzPicture();
            tikzPicture.Bind(scene);

            return tikzPicture;
        }
    }
}