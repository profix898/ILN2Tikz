using System;
using System.Collections.Generic;
using ILNumerics.Drawing;

namespace ILN2Tikz.Generator.Global
{
    public sealed class PGFPlotOptions : List<string>, ITikzElement
    {
        public PGFPlotOptions()
        {
            Add("set layers"); // Sort layers
            Add("major grid style={solid,very thin,white!80!black}"); // Default major grid style
            Add("minor grid style={dashed,very thin,white!90!black}"); // Default minor grid style
        }

        #region Implementation of ITikzElement

        public string PreTag
        {
            get { return ""; }
        }

        public IEnumerable<string> Content
        {
            get
            {
                // PGFPlots Options
                foreach (var option in this)
                    yield return $"\\pgfplotsset{{{option}}}";
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
    }
}