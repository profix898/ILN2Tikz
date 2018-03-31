using System;
using System.Collections.Generic;
using System.Drawing;
using ILNumerics.Drawing;

namespace ILN2Tikz.Generator.Global
{
    public class TikzColors : List<Color>, ITikzElement
    {
        #region Implementation of ITikzElement

        public string PreTag
        {
            get { return ""; }
        }

        public IEnumerable<string> Content
        {
            get
            {
                // Color Definitions
                for (var i = 0; i < Count; i++)
                {
                    var colorName = $"colorDef{i:D2}";
                    var color = GetTikzColor(this[i]);

                    yield return $"\\definecolor{{{colorName}}}{{rgb}}{{{color}}}";
                }
            }
        }

        public string PostTag
        {
            get { return ""; }
        }

        public void Bind(ILNode node)
        {
            // NOTE: Don't bind automatically
            throw new NotSupportedException();
        }

        #endregion

        public string GetColorName(Color color)
        {
            return $"colorDef{IndexOf(color):D2}";
        }

        public string GetTikzColor(Color color)
        {
            var r = color.R / 255.0;
            var g = color.G / 255.0;
            var b = color.B / 255.0;

            return FormattableString.Invariant($"{r:F6},{g:F6},{b:F6}");
        }
    }
}