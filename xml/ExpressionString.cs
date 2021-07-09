using System.Collections.Generic;
using System.Linq;
using System.Text;
using XElementPath;

namespace System.Xml
{
    /// <summary>
    /// a Function for Strings, will be used as Default
    /// </summary>
    public class ExpressionString : ExpressionStringBase
    {
        #region Properties

        /// <summary>
        /// The Expression of the Function
        /// </summary>
        public new Expression Expression { get; set; }

        /// <summary>
        /// The Function of the Expression
        /// </summary>
        public FunctionBase Function { get; set; }

        /// <summary>
        /// the NEct Expression
        /// </summary>
        public ExpressionString NextExpression { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="ExpressionString"/>
        /// </summary>
        /// <param name="content">Content of the Function</param>
        public ExpressionString(string content) : base(content)
        {
            if (String.IsNullOrEmpty(content))
            {
                content = "//";
            }
            content = content.Trim();

            if (content.StartsWith("'"))
            {
                base.Expression = content.Substring(1, content.Length - 2);
            }
            else
            {
                var conditionBuilder = new StringBuilder();
                char item;
                for (int i = 0; i < content.Length; i++)
                {
                    item = content[i];

                    conditionBuilder.Append(item);
                    if (Manager.Functions.FirstOrDefault(x => x.Name.StartsWith(conditionBuilder.ToString())) is FunctionBase fu)
                    {
                        Function = fu;
                    }
                }
                if (Function != null)
                {
                    NextExpression = new ExpressionString(content.Substring(Function.Name.Length).TrimEnd(')'));
                }
                else
                {
                    Expression = new Expression(content);
                }
            }
        }

        #endregion Constructors

        #region Methods

        /// <inheritdoc cref="GetValues"/>
        protected IEnumerable<string> InternalGetValues(XmlLinkedNode node)
        {
            if (NextExpression != null)
            {
                foreach (var item in NextExpression.InternalGetValues(node))
                {
                    yield return Function?.Process(item) ?? item;
                }
            }
            else if (Expression == null)
            {
                yield return base.Expression;
            }
            else
            {
                foreach (var item in Expression.GetElements(node))
                {
                    if (item is XmlText xt)
                    {
                        yield return Function?.Process(xt.InnerText) ?? xt.InnerText;
                    }
                    else if (item is XmlElement xe)
                    {
                        yield return xe.InnerText;
                    }
                    else
                    {
                        yield return Function?.Process(item.ToString()) ?? item.ToString();
                    }
                }
            }
        }

        /// <inheritdoc/>
        public override IEnumerable<string> GetValues(XmlLinkedNode node)
        {
            return InternalGetValues(node);
        }

        #endregion Methods
    }
}