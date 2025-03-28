using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using ILNumerics.Drawing;

namespace ILN2Tikz.Generator.Global;

public class TikzColors : ICollection<Color>, ITikzElement
{
    #region KnownColors

    private readonly Dictionary<Color, string> _knownColors = new Dictionary<Color, string>
    {
        { Color.Black, "black" },
        { Color.White, "white" }
    };

    #endregion

    private readonly List<Color> _colors = new List<Color>();

    #region ICollection<Color>

    public int Count => _colors.Count;

    public bool IsReadOnly => false;

    public void Add(Color item)
    {
        if (_knownColors.ContainsKey(item))
            return;

        // Prevent duplicates (only add once)
        if (_colors.Contains(item))
            return;

        _colors.Add(item);
    }

    public void Clear()
    {
        _colors.Clear();
    }

    public bool Contains(Color item)
    {
        if (_knownColors.ContainsKey(item))
            return true;

        return _colors.Contains(item);
    }

    public void CopyTo(Color[] array, int arrayIndex)
    {
        _colors.CopyTo(array, arrayIndex);
    }

    public bool Remove(Color item)
    {
        return _colors.Remove(item);
    }

    public IEnumerator<Color> GetEnumerator()
    {
        return _colors.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable) _colors).GetEnumerator();
    }

    #endregion

    #region Implementation of ITikzElement

    public string PreTag => "";

    public IEnumerable<string> Content
    {
        get
        {
            // Color Definitions
            for (var i = 0; i < Count; i++)
            {
                var colorName = $"colorDef{i:D2}";
                var color = FormatTikzColor(_colors[i]);

                yield return $"\\definecolor{{{colorName}}}{{rgb}}{{{color}}}";
            }
        }
    }

    public string PostTag => "";

    public void Bind(Node node, TikzGlobals globals)
    {
        // NOTE: Don't bind automatically
        throw new NotSupportedException();
    }

    #endregion

    public string GetColorName(Color color)
    {
        if (_knownColors.ContainsKey(color))
            return _knownColors[color];

        return $"colorDef{_colors.IndexOf(color):D2}";
    }

    public string FormatTikzColor(Color color)
    {
        var r = color.R / 255.0;
        var g = color.G / 255.0;
        var b = color.B / 255.0;

        return FormattableString.Invariant($"{r:F6},{g:F6},{b:F6}");
    }
}