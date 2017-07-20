using System;
using System.Collections.Generic;
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

        public void Write(params ITikzElement[] elements)
        {
            Write((ICollection<ITikzElement>) elements);
        }

        public void Write(IEnumerable<ITikzElement> elements)
        {
            var container = new TikzElementContainer(elements);

            // Render Content (line-by-line)
            foreach (var contentLine in container.Content)
                writer.WriteLine(contentLine);
        }

        #region Nested type: TikzElementContainer

        private class TikzElementContainer : TikzElementBase
        {
            #region Overrides of TikzElementBase

            public TikzElementContainer(IEnumerable<ITikzElement> elements)
            {
                foreach (var element in elements)
                    Add(element);
            }

            public override string PreTag
            {
                get { return String.Empty; }
            }

            public override string PostTag
            {
                get { return String.Empty; }
            }

            #endregion
        }

        #endregion
    }
}
