using System.Collections.Generic;
using System.Linq;
using System.Text;
using XElementPath;

namespace System.Xml
{
    /// <summary>
    /// Contains a Expression Condition used to Filter Extracted XML values
    /// </summary>
    public class ExpressionCondition : ExpressionStringBase
    {
        #region Properties

        /// <summary>
        /// The next Condition in the Queue
        /// </summary>
        public ExpressionCondition NextCondition { get; set; }

        /// <summary>
        /// The operator that connects the Conditions
        /// </summary>
        public OperatorBase NextConditionOperator { get; set; }

        /// <summary>
        /// The Function on the left side
        /// </summary>
        public ExpressionStringBase LeftFunction { get; set; }

        /// <summary>
        /// The Function on the Right side
        /// </summary>
        public ExpressionStringBase RightFunction { get; set; }

        /// <summary>
        /// The Conditions Operator
        /// </summary>
        public OperatorBase Operator { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="ExpressionCondition"/>
        /// </summary>
        /// <param name="generate"></param>
        public ExpressionCondition(string generate) : base(generate)
        {
            generate = generate.Trim();
            Expression = generate;
            var blockBuilder = new StringBuilder();
            var conditionChecker = new StringBuilder();
            char item;
            for (var i = 0; i < generate.Length; i++)
            {
                item = generate[i];

                conditionChecker.Append(item);
                if (Manager.Operators.FirstOrDefault(x => x.Name.StartsWith(conditionChecker.ToString())) is OperatorBase co)
                {
                    if (co.Name.Length == conditionChecker.Length)
                    {
                        if (LeftFunction != null)
                        {
                            RightFunction = new ExpressionString(blockBuilder.ToString().Substring(0, blockBuilder.Length - (conditionChecker.Length - 1)).Trim());
                            NextCondition = new ExpressionCondition(generate.Substring(i + 1));
                            NextConditionOperator = co;
                            break;
                        }
                        else
                        {
                            Operator = co;
                            LeftFunction = new ExpressionString(blockBuilder.ToString().Trim());
                            i++;
                            blockBuilder.Clear();
                            continue;
                        }
                    }
                }
                else
                {
                    conditionChecker.Clear();
                }

                blockBuilder.Append(item);
            }
            if (RightFunction == null)
            {
                RightFunction = new ExpressionString(blockBuilder.ToString().Trim());
            }
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Evaluates the condition
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool Evaluate(XmlLinkedNode node)
        {
            if (NextConditionOperator != null)
            {
                return NextConditionOperator.Check(node, this, NextCondition);
            }

            return Operator.Check(node, LeftFunction, RightFunction);
        }

        /// <summary>
        /// returns the Conditions result
        /// </summary>
        /// <param name="node">the node that will be checked</param>
        /// <returns>the Conditions result</returns>
        public override IEnumerable<string> GetValues(XmlLinkedNode node)
        {
            yield return Operator.Check(node, LeftFunction, RightFunction) ? "1" : "0";
        }

        #endregion Methods
    }
}