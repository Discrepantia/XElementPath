namespace System.Xml
{
    /// <summary>
    /// Or '||' Operator implementation
    /// </summary>
    public class OrOperator : OperatorBase
    {
        #region Methods

        /// <inheritdoc/>
        public override bool InnerCheck(string left, string right)
        {
            return left == "1" || right == "1";
        }

        /// <inheritdoc/>
        public override string GetName()
        {
            return "||";
        }

        #endregion Methods
    }
}