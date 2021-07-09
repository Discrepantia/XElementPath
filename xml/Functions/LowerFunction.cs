namespace System.Xml
{
    /// <summary>
    /// lower() function implementation, Returns a copy of this string converted to lowercase
    /// </summary>
    public class LowerFunction : FunctionBase
    {
        #region Methods

        /// <inheritdoc/>
        public override string GetFunctionName()
        {
            return "lower";
        }

        /// <inheritdoc/>
        public override string Process(string item)
        {
            return item.ToLower();
        }

        #endregion Methods
    }
}