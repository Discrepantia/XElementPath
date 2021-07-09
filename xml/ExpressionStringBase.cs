using System.Collections.Generic;

namespace System.Xml
{
    /// <summary>
    /// Base Class for Operators
    /// </summary>
    public abstract class ExpressionStringBase
    {
        #region Properties

        /// <summary>
        /// The Operators expression
        /// </summary>
        public string Expression { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="ExpressionStringBase"/>
        /// </summary>
        /// <param name="content">Content of the Expression</param>
        protected ExpressionStringBase(string content)
        {
            Expression = content;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Returns all Processed values of the function
        /// </summary>
        /// <param name="node">The Node that will be processed</param>
        /// <returns>all Processed values of the function</returns>
        public abstract IEnumerable<string> GetValues(XmlLinkedNode node);

        #endregion Methods
    }
}