using System.Collections.Generic;
using System.Text;

namespace System.Xml
{
    /// <summary>
    /// Contains a Expression used to Extract XML values
    /// </summary>
    public class Expression
    {
        #region Properties

        /// <summary>
        /// The next Expression
        /// </summary>
        public Expression NextExpression { get; set; }

        /// <summary>
        /// The current Expression Part
        /// </summary>
        public string ExpressionStep { get; set; }

        /// <summary>
        /// The whole Expression
        /// </summary>
        public string ExpressionString { get; }

        /// <summary>
        /// The Condition of the current Expression
        /// </summary>
        public ExpressionCondition Condition { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="ExpressionString"/>
        /// </summary>
        /// <param name="Expression">The expression string</param>
        public Expression(string expression)
        {
            ExpressionString = expression;
            ExpressionString = ExpressionString.Replace("\r\n", "").Replace("\n", "");
            var seperator = '/';
            var braceOpen = '[';
            var braceClose = ']';

            var blockBuilder = new StringBuilder();

            var skippy = 0;
            char item;
            if (ExpressionString.StartsWith("//"))
            {
                ExpressionStep = "//";
                ExpressionString = ExpressionString.Substring(1);
            }

            for (var i = 0; i < ExpressionString.Length; i++)
            {
                item = ExpressionString[i];

                if (item == braceOpen && skippy == 0)
                {
                    skippy++;
                    ExpressionStep = blockBuilder.ToString();
                    blockBuilder.Clear();
                    continue;
                }
                if (item == braceClose)
                {
                    skippy--;
                    if (skippy == 0)
                    {
                        Condition = new ExpressionCondition(blockBuilder.ToString());
                        blockBuilder.Clear();
                    }
                }

                if (item == seperator && skippy == 0)
                {
                    NextExpression = new Expression(ExpressionString.Substring(i + 1));
                    break;
                }
                blockBuilder.Append(ExpressionString[i]);
            }

            if (String.IsNullOrEmpty(ExpressionStep))
            {
                ExpressionStep = blockBuilder.ToString();
                if (ExpressionStep == braceClose.ToString())
                {
                    ExpressionStep = "";
                }
            }
            else if (blockBuilder.Length != 0)
            {
                if (Condition != null)
                {
                    if (blockBuilder.ToString() != braceClose.ToString())
                    {
                        Condition.NextCondition = new ExpressionCondition(blockBuilder.ToString().Substring(1));

                        Condition.NextConditionOperator = new AndOperator();
                    }
                }
            }
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Returns the Expressions Child elements
        /// </summary>
        /// <param name="source">the source Node</param>
        /// <returns>the Expressions Child elements</returns>
        public IEnumerable<object> GetElements(object source)
        {
            if (source is XmlElement node)
            {
                if (ExpressionStep == "//")
                {
                    foreach (var _child in node.IterateAll())
                    {
                        foreach (var nextItem in NextExpression.GetElements(_child))
                        {
                            yield return nextItem;
                        }
                    }
                }
                if (ExpressionStep == ".")
                {
                    yield return node.InnerXml;
                }
                if (ExpressionStep == ":")
                {
                    yield return node.InnerText;
                }
                else if (ExpressionStep.StartsWith("@"))
                {
                    if (node.HasAttribute(ExpressionStep.Substring(1)))
                    {
                        yield return node.GetAttribute(ExpressionStep.Substring(1)).ToString();
                    }
                }
                else
                {
                    var en = node.ChildNodes.ToEnummerable();

                    if (!String.IsNullOrEmpty(ExpressionStep))
                    {
                        en = node.GetChildrenByName(ExpressionStep);
                    }
                    foreach (var item in en)
                    {
                        if (Condition == null || Condition.Evaluate(item))
                        {
                            if (NextExpression != null)
                            {
                                foreach (var nextItem in NextExpression.GetElements(item))
                                {
                                    yield return nextItem;
                                }
                            }
                            else
                            {
                                yield return source;
                            }
                        }
                    }
                }
            }
        }

        #endregion Methods
    }
}