using System;
using System.Collections;
using System.Collections.Generic;
using ILN2Tikz.Generator.Global;
using ILNumerics.Drawing;

namespace ILN2Tikz.Generator
{
    public abstract class TikzGroupElementBase : ITikzElement, ICollection<ITikzElement>, ITikzGroupElement
    {
        private readonly List<ITikzElement> children = new List<ITikzElement>();
        
        #region Implementation of ITikzElement

        public abstract string PreTag { get; }

        public virtual IEnumerable<string> Content
        {
            get
            {
                // Return elements (one-by-one)
                foreach (var element in children)
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

        public virtual void Bind(ILNode node, Globals globals)
        {
            // Not needed for a group element
        }

        #endregion

        #region Implementation of ITikzGroupElement

        public abstract void Bind(ILGroup @group, Globals globals);

        #endregion

        #region Implementation of IEnumerable

        public IEnumerator<ITikzElement> GetEnumerator()
        {
            return children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) children).GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<ITikzElement>

        public void Add(ITikzElement item)
        {
            children.Add(item);
        }

        public void Clear()
        {
            children.Clear();
        }

        public bool Contains(ITikzElement item)
        {
            return children.Contains(item);
        }

        public void CopyTo(ITikzElement[] array, int arrayIndex)
        {
            children.CopyTo(array, arrayIndex);
        }

        public bool Remove(ITikzElement item)
        {
            return children.Remove(item);
        }

        public int Count
        {
            get { return children.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        #endregion
    }
}
