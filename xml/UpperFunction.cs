namespace System.Xml
{

        /// <summary>
        /// upper() function implementation, Returns a copy of this string converted to uppercase
        /// </summary>
        public class UpperFunction : FunctionBase
        {

        #region Methods

        /// <inheritdoc/>
        public override string GetFunctionName()
            {
                return "upper";
            }

            /// <inheritdoc/>
            public override string Process(string item)
            {
                return item.ToUpper();
        }

        #endregion

    }

}
