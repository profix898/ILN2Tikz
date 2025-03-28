using System;
using System.IO;
using ILNumerics.Drawing;
using TikzExport.Generator;
using TikzExport.Generator.Elements;
using TikzExport.Generator.Global;
using Size = System.Drawing.Size;

namespace TikzExport;

public static class TikzExport
{
    public static string ExportString(Scene scene, Size canvasSize = default)
    {
        if (scene == null)
            throw new ArgumentNullException(nameof(scene));

        using (var stringWriter = new StringWriter())
        {
            Export(scene, stringWriter, canvasSize);

            return stringWriter.ToString();
        }
    }

    public static void ExportFile(Scene scene, string filePath, Size canvasSize = default)
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

    public static void Export(Scene scene, TextWriter writer, Size canvasSize = default)
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

    public static TikzPicture Bind(Scene scene, Size canvasSize = default)
    {
        if (scene == null)
            throw new ArgumentNullException(nameof(scene));

        scene.Configure();

        var tikzPicture = new TikzPicture(canvasSize);
        tikzPicture.Bind(scene, new TikzGlobals());

        return tikzPicture;
    }
}