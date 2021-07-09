using System.Linq;

namespace System.Xml
{
    /// <summary>
    /// Base Class for Operators Conditions
    /// </summary>
    public abstract class OperatorBase
    {
        #region Properties

        /// <summary>
        /// Contains the Name of the Operator
        /// </summary>
        public string Name { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="OperatorBase"/>
        /// </summary>
        protected OperatorBase()
        {
            Name = GetName();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Returns the NAme of the Operator
        /// </summary>
        /// <returns>Name of the Operator</returns>
        public abstract string GetName();

        /// <summary>
        /// Checks the Operator
        /// </summary>
        /// <param name="node">the Node that will be checked</param>
        /// <param name="left">the left function</param>
        /// <param name="right">the right function</param>
        /// <returns>the Operators result</returns>
        public bool Check(XmlLinkedNode node, ExpressionStringBase left, ExpressionStringBase right)
        {
            foreach (var item in left.GetValues(node))
            {
                if (right.GetValues(node).Any(x => InnerCheck(x, item)))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks the Operator
        /// </summary>
        /// <param name="left">the left string</param>
        /// <param name="right">the right string</param>
        /// <returns>the Operators result</returns>
        public abstract bool InnerCheck(string left, string right);

        #endregion Methods
    }
}