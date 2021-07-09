using System.Collections.Generic;
using System.Xml;

namespace XElementPath
{
    /// <summary>
    /// Contains information about every valid Function
    /// </summary>
    public static class Manager
    {
        /// <summary>
        /// Contains all implemented Functions
        /// </summary>
        public static List<FunctionBase> Functions { get; }

        /// <summary>
        /// Gets a List of all Programmed conditions
        /// </summary>
        public static List<OperatorBase> Operators { get; }

        /// <summary>
        /// Initializes the <see cref="Manager"/>
        /// </summary>
        static Manager()
        {
            Functions = new List<FunctionBase>();
            Functions.AddRange(LoadBasicFunctions());

            Operators = new List<OperatorBase>();
            Operators.AddRange(LoadBasicOperatos());
        }

        /// <summary>
        /// Iterates all implemented Functions
        /// </summary>
        /// <returns>all implemented Functions</returns>
        private static IEnumerable<FunctionBase> LoadBasicFunctions()
        {
            yield return new LowerFunction();
            yield return new UpperFunction();
        }

        /// <summary>
        /// Iterates all implemented Operators
        /// </summary>
        /// <returns>all implemented Operators</returns>
        private static IEnumerable<OperatorBase> LoadBasicOperatos()
        {
            yield return new EqualOperator();
            yield return new OrOperator();
            yield return new AndOperator();
        }
    }
}