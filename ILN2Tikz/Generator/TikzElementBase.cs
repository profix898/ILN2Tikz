using System.Collections;
using System.Collections.Generic;

namespace ILN2Tikz.Generator
{
    public abstract class TikzElementBase : ITikzElement, ICollection<ITikzElement>
    {
        private readonly List<ITikzElement> childElements = new List<ITikzElement>();

        #region Implementation of ITikzElement

        public abstract string PreTag { get; }

        public virtual IEnumerable<string> Content
        {
            get
            {
                // Return elements (one-by-one)
                foreach (var element in childElements)
                {
                    // Return PreTag of the nth element
                    if (!string.IsNullOrEmpty(element.PreTag))
                        yield return element.PreTag;

                    // Return content of the nth element (line-by-line)
                    foreach (var contentLine in element.Content)
                        yield return contentLine;

                    // Return PostTag of the nth element
                    if (!string.IsNullOrEmpty(element.PostTag))
                        yield return element.PostTag;
                }
            }
        }

        public abstract string PostTag { get; }

        #endregion

        #region Implementation of IEnumerable

        public IEnumerator<ITikzElement> GetEnumerator()
        {
            return childElements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) childElements).GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<ITikzElement>

        public void Add(ITikzElement item)
        {
            childElements.Add(item);
        }

        public void Clear()
        {
            childElements.Clear();
        }

        public bool Contains(ITikzElement item)
        {
            return childElements.Contains(item);
        }

        public void CopyTo(ITikzElement[] array, int arrayIndex)
        {
            childElements.CopyTo(array, arrayIndex);
        }

        public bool Remove(ITikzElement item)
        {
            return childElements.Remove(item);
        }

        public int Count
        {
            get { return childElements.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        #endregion
    }
}
