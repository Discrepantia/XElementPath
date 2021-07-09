using System.Collections.Generic;
using System.Linq;

namespace System.Xml
{
    /// <summary>
    /// Extension class for XmlElement
    /// </summary>
    public static partial class XmlElementExtension
    {
        #region Methods

        /// <summary>
        /// Extracts the selected node.
        /// </summary>
        /// <param name="source">the extension source</param>
        /// <param name="expression">the path expression</param>
        /// <returns>extracted nodes</returns>
        public static IEnumerable<object> GetElements(this XmlDocument source, string expression)
        {
            var step = new Expression(expression);
            foreach (var item in step.GetElements(source.DocumentElement))
            {
                yield return item;
            }
        }

        /// <summary>
        /// Extracts the selected node.
        /// </summary>
        /// <param name="source">the extension source</param>
        /// <param name="expression">the path expression</param>
        /// <returns>extracted nodes</returns>
        public static object GetFirstElement(this XmlDocument source, string expression)
        {
            return source.GetElements(expression).FirstOrDefault();
        }

        /// <summary>
        /// Extracts the selected node.
        /// </summary>
        /// <param name="source">the extension source</param>
        /// <param name="path">the path expression</param>
        /// <returns>extracted nodes</returns>
        public static IEnumerable<object> GetElements(this XmlElement source, string path)
        {
            var step = new Expression(path);
            foreach (var item in step.GetElements(source))
            {
                yield return item;
            }
        }

        /// <summary>
        /// Extracts the selected node.
        /// </summary>
        /// <param name="source">the extension source</param>
        /// <param name="path">the path expression</param>
        /// <returns>extracted nodes</returns>
        public static object GetFirstElement(this XmlElement source, string path)
        {
            return source.GetElements(path).FirstOrDefault();
        }

        /// <summary>
        /// Extracts the Children by their name. use * as wild card
        /// </summary>
        /// <param name="source">the extension source</param>
        /// <param name="nodeName">the name of the node</param>
        /// <returns>extracted nodes</returns>
        public static IEnumerable<XmlLinkedNode> GetChildrenByName(this XmlElement source, string nodeName)
        {
            if (nodeName == "*")
            {
                foreach (var item in source.ToEnummerable())
                {
                    yield return item;
                }
            }
            else
            {
                foreach (var item in source.ToEnummerable())
                {
                    if (item.Name == nodeName)
                    {
                        yield return item;
                    }
                }
            }
        }

        /// <summary>
        /// makes <see cref="XmlElement"/> to <see cref="IEnumerable{XmlLinkedNode}"/>
        /// </summary>
        /// <param name="source">the extension source</param>
        /// <returns><see cref="IEnumerable{XmlLinkedNode}"/></returns>
        public static IEnumerable<XmlLinkedNode> ToEnummerable(this XmlElement source)
        {
            var en = source.GetEnumerator();
            while (en.MoveNext())
            {
                if (en.Current is XmlElement)
                {
                    yield return (XmlElement)en.Current;
                }
            }
        }

        /// <summary>
        /// makes <see cref="XmlNodeList"/> to <see cref="IEnumerable{XmlLinkedNode}"/>
        /// </summary>
        /// <param name="source">the extension source</param>
        /// <returns><see cref="IEnumerable{XmlLinkedNode}"/></returns>
        public static IEnumerable<XmlLinkedNode> ToEnummerable(this XmlNodeList source)
        {
            foreach (var item in source.OfType<XmlLinkedNode>())
            {
                yield return item;
            }
        }

        /// <summary>
        /// Iterates through all Children of a Node
        /// </summary>
        /// <param name="source">the extension source</param>
        /// <returns>all Child Nodes</returns>
        public static IEnumerable<XmlElement> IterateAll(this XmlElement source)
        {
            yield return source;
            foreach (var child in source.ChildNodes)
            {
                if (child is XmlElement xe)
                {
                    foreach (var _child in IterateAll(xe))
                    {
                        yield return _child;
                    }
                }
            }
        }

        #endregion Methods
    }
}