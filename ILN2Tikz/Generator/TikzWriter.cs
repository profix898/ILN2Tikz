using System.IO;

namespace ILN2Tikz.Generator
{
    public class TikzWriter
    {
        private readonly TextWriter writer;

        public TikzWriter(TextWriter writer)
        {
            this.writer = writer;
        }

        public void Write(TikzPicture picture)
        {
            writer.WriteLine(picture.PreTag);

            // Render Content (line-by-line)
            foreach (var contentLine in picture.Content)
                writer.WriteLine(contentLine);

            writer.WriteLine(picture.PostTag);
        }
    }
}