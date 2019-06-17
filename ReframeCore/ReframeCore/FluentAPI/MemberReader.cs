using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.FluentAPI
{
    public static class MemberReader
    {
        private static readonly string expressionCannotBeNullMessage = "The expression cannot be null.";
        private static readonly string invalidExpressionMessage = "Invalid or not supported expression.";

        #region GetMemberName

        public static string GetMemberName<T>(Expression<Func<T, object>> expression)
        {
            if (expression == null)
            {
                throw new FluentException(expressionCannotBeNullMessage);
            }

            return GetMemberName(expression.Body);
        }

        public static string GetMemberName<T>(Expression<Func<T>> expression)
        {
            if (expression == null)
            {
                throw new FluentException(expressionCannotBeNullMessage);
            }

            return GetMemberName(expression.Body);
        }

        public static string GetMemberName(Expression<Action> expression)
        {
            if (expression == null)
            {
                throw new FluentException(expressionCannotBeNullMessage);
            }

            return GetMemberName(expression.Body);
        }

        private static string GetMemberName(Expression expression)
        {
            string memberName = "";

            try
            {
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
            }
            catch
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
                MemberExpression temp = (MemberExpression)unaryExpression.Operand;
                MemberInfo memberInfo = (temp.Expression as MemberExpression).Member;
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

        #endregion

        #region GetMemberOwner

        public static object GetMemberOwner(Expression<Func<object>> expression)
        {
            if (expression == null)
            {
                throw new FluentException(expressionCannotBeNullMessage);
            }

            return GetMemberOwner(expression.Body);
        }

        public static object GetMemberOwner(Expression<Action> expression)
        {
            if (expression == null)
            {
                throw new FluentException(expressionCannotBeNullMessage);
            }

            return GetMemberOwner(expression.Body);
        }

        private static object GetMemberOwner(MemberExpression expression)
        {
            object owner = null;
            MemberExpression ex = expression as MemberExpression;

            string[] memberPath = GetMemberPath(expression);         
            while (ex.Expression is MemberExpression)
            {
                ex = ex.Expression as MemberExpression;
            }

            var cEx = ex.Expression as ConstantExpression;

            if (memberPath.Length > 2)
            {
                var t = cEx.Value.GetType().InvokeMember(memberPath[memberPath.Length - 1], BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public, null, cEx.Value, null);
                return t;
            }

            owner = cEx.Value;

            return owner;
        }

        private static object GetMemberOwner(UnaryExpression expression)
        {
            object owner = null;

            string[] memberPath = GetMemberPath(expression);
            var uex = expression as UnaryExpression;
            var ex = uex.Operand as MemberExpression;
            while (ex.Expression is MemberExpression)
            {
                ex = ex.Expression as MemberExpression;
            }
            var cEx = ex.Expression as ConstantExpression;

            owner = cEx.Value;

            if (memberPath.Length > 2)   //In case path contains Member, parent object, parent object's parent etc. E.g. if the expression is in the form of () => Project.Zone.Floor.Width.
            {
                int counter = memberPath.Length - 2;
                while (counter > 0)
                {
                    owner = owner.GetType().InvokeMember(memberPath[counter], BindingFlags.GetField | BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, owner, null);
                    counter--;
                }
            }

            return owner;
        }

        private static object GetMemberOwner(MethodCallExpression expression)
        {
            object owner = null;
            string[] memberPath = GetMemberPath(expression);
            var mex = expression as MethodCallExpression;
            var ex = mex.Object as MemberExpression;

            while (ex.Expression is MemberExpression)
            {
                ex = ex.Expression as MemberExpression;
            }

            var cEx = ex.Expression as ConstantExpression;
            owner = cEx.Value;
            if (memberPath.Length > 2)
            {
                int counter = memberPath.Length - 2;
                while (counter > 0)
                {
                    owner = owner.GetType().InvokeMember(memberPath[counter], BindingFlags.GetField | BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, owner, null);
                    counter--;
                }
            }

            return owner;
        }

        private static object GetMemberOwner(Expression expression)
        {
            object owner = null;

            if (expression is MemberExpression)
            {
                owner = GetMemberOwner(expression as MemberExpression);
            }
            else if (expression is UnaryExpression)
            {
                owner = GetMemberOwner(expression as UnaryExpression);
            }
            else if (expression is MethodCallExpression)
            {
                owner = GetMemberOwner(expression as MethodCallExpression);
            }

            return owner;
        }

        #endregion

        #region GetMemberPath

        private static string[] GetMemberPath(MemberExpression expression)
        {
            List<string> path = new List<string>();

            MemberExpression ex = expression;
            path.Add(ex.Member.Name);
            while (ex.Expression is MemberExpression)
            {
                ex = ex.Expression as MemberExpression;
                path.Add(ex.Member.Name);
            }

            ConstantExpression cEx = ex.Expression as ConstantExpression;
            path.Add(cEx.Type.Name);

            return path.ToArray();
        }

        private static string[] GetMemberPath(UnaryExpression expression)
        {
            List<string> path = new List<string>();

            UnaryExpression uex = expression as UnaryExpression;
            MemberExpression ex = uex.Operand as MemberExpression;
            path.Add(ex.Member.Name);

            while (ex.Expression is MemberExpression)
            {
                ex = ex.Expression as MemberExpression;
                path.Add(ex.Member.Name);
            }
            ConstantExpression cEx = ex.Expression as ConstantExpression;
            path.Add(cEx.Type.Name);

            return path.ToArray();
        }

        private static string[] GetMemberPath(MethodCallExpression expression)
        {
            List<string> path = new List<string>();

            MethodCallExpression mex = expression as MethodCallExpression;
            path.Add(mex.Method.Name);
            MemberExpression ex = mex.Object as MemberExpression;
            path.Add(ex.Member.Name);
            while (ex.Expression is MemberExpression)
            {
                ex = ex.Expression as MemberExpression;
                path.Add(ex.Member.Name);
            }
            ConstantExpression cEx = ex.Expression as ConstantExpression;
            path.Add(cEx.Type.Name);

            return path.ToArray();
        }

        #endregion
    }
}
