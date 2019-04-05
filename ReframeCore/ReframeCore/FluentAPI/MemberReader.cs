using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.FluentAPI
{
    public static class MemberReader
    {
        private static readonly string expressionCannotBeNullMessage = "The expression cannot be null.";
        private static readonly string invalidExpressionMessage = "Invalid or not supported expression.";

        public static string GetMemberName(Expression expression)
        {
            string memberName = "";

            if (expression == null)
            {
                throw new FluentException(expressionCannotBeNullMessage);
            }

            if (expression is UnaryExpression)
            {
                memberName = GetMemberName((UnaryExpression)expression);
            }

            if (expression is MemberExpression)
            {
                memberName = GetMemberName((MemberExpression)expression);
            }

            if (expression is MethodCallExpression)
            {
                memberName = GetMemberName((MethodCallExpression)expression);
            }

            if (memberName == "")
            {
                throw new FluentException(invalidExpressionMessage);
            }

            return memberName;
        }

        private  static string GetMemberName(UnaryExpression unaryExpression)
        {
            string memberName = "";

            if (unaryExpression.Operand is MethodCallExpression)
            {
                var methodExpression = (MethodCallExpression)unaryExpression.Operand;
                memberName = methodExpression.Method.Name;
            }
            else
            {
                memberName = ((MemberExpression)unaryExpression.Operand).Member.Name;
            }

            return memberName;
        }

        private static string GetMemberName(MemberExpression memberExpression)
        {
            return memberExpression.Member.Name;
        }

        private static string GetMemberName(MethodCallExpression methodExpression)
        {
            return methodExpression.Method.Name;
        }
    }
}
