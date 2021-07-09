namespace System.Xml
{
    /// <summary>
    /// Equal '=' Operator implementation
    /// </summary>
    public class EqualOperator : OperatorBase
    {
        #region Methods

        /// <inheritdoc/>
        public override bool InnerCheck(string left, string right)
        {
            return left == right;
        }

        /// <inheritdoc/>
        public override string GetName()
        {
            return "=";
        }

        #endregion Methods
    }
}