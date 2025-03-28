using System;
using System.Collections;
using System.Collections.Generic;
using ILNumerics.Drawing;
using TikzExport.Generator.Global;

namespace TikzExport.Generator;

public abstract class TikzGroupElementBase : ITikzElement, ICollection<ITikzElement>, ITikzGroupElement
{
    private readonly List<ITikzElement> _children = new List<ITikzElement>();
        
    #region Implementation of ITikzElement

    public abstract string PreTag { get; }

    public virtual IEnumerable<string> Content
    {
        get
        {
            // Return elements (one-by-one)
            foreach (var element in _children)
            {
                // Return PreTag of the nth element
                if (!String.IsNullOrEmpty(element.PreTag))
                    yield return element.PreTag;

                // Return content of the nth element (line-by-line)
                foreach (var contentLine in element.Content)
                    yield return contentLine;

                // Return PostTag of the nth element
                if (!String.IsNullOrEmpty(element.PostTag))
                    yield return element.PostTag;
            }
        }
    }

    public abstract string PostTag { get; }

    public virtual void Bind(Node node, TikzGlobals globals)
    {
        // Not needed for a group element
    }

    #endregion

    #region Implementation of ITikzGroupElement

    public abstract void Bind(Group group, TikzGlobals globals);

    #endregion

    #region Implementation of IEnumerable

    public IEnumerator<ITikzElement> GetEnumerator()
    {
        return _children.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable) _children).GetEnumerator();
    }

    #endregion

    #region Implementation of ICollection<ITikzElement>

    public void Add(ITikzElement item)
    {
        _children.Add(item);
    }

    public void Clear()
    {
        _children.Clear();
    }

    public bool Contains(ITikzElement item)
    {
        return _children.Contains(item);
    }

    public void CopyTo(ITikzElement[] array, int arrayIndex)
    {
        _children.CopyTo(array, arrayIndex);
    }

    public bool Remove(ITikzElement item)
    {
        return _children.Remove(item);
    }

    public int Count
    {
        get { return _children.Count; }
    }

    public bool IsReadOnly
    {
        get { return false; }
    }

    #endregion
}