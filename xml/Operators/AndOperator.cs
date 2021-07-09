namespace System.Xml
{
    /// <summary>
    /// And '&amp;&amp;' Operator implementation
    /// </summary>
    public class AndOperator : OperatorBase
    {
        #region Methods

        /// <inheritdoc/>
        public override bool InnerCheck(string left, string right)
        {
            return left == "1" && right == "1";
        }

        /// <inheritdoc/>
        public override string GetName()
        {
            return "&&";
        }

        #endregion Methods
    }
}