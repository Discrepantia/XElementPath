namespace System.Xml
{

    /// <summary>
    /// BaseClass for Functions
    /// </summary>
    public abstract class FunctionBase
    {

        #region Properties

        /// <summary>
        /// Name of the Function
        /// </summary>
        public string Name { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// initializes a new instance of <see cref="FunctionBase"/>
        /// </summary>
        protected FunctionBase()
        {
            //A Function ends with a '(' than its easy to extract from a string
            Name = GetFunctionName() + "(";
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the Name of the Function
        /// </summary>
        /// <returns>the Name of the Function</returns>
        public abstract string GetFunctionName();

        /// <summary>
        /// Process the Function
        /// </summary>
        /// <param name="item">the Item that will be processed</param>
        /// <returns>the result of the function</returns>
        public abstract string Process(string item);

        #endregion

    }

}
