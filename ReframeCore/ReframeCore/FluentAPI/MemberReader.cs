﻿using System;
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

        public static object GetMemberOwner<T>(Expression<Func<T>> expression)
        {
            if (expression == null)
            {
                throw new FluentException(expressionCannotBeNullMessage);
            }

            return GetMemberOwner(expression.Body);
        }

        private static object GetMemberOwner(Expression expression)
        {
            object owner = null;

            if (expression is MemberExpression)
            {
                string[] memberPath = GetMemberPath(expression);

                MemberExpression ex = expression as MemberExpression;
                while (ex.Expression is MemberExpression)
                {
                    ex = ex.Expression as MemberExpression;
                }

                var cEx = ex.Expression as ConstantExpression;

                if (memberPath.Length > 2)
                {
                    var t = cEx.Value.GetType().InvokeMember(memberPath[1], BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public, null, cEx.Value, null);
                    return t;
                }

                owner = cEx.Value;
            }
            else if (expression is UnaryExpression)
            {
                object parentObject = null;

                string[] memberPath = GetMemberPath(expression);
                var uex = expression as UnaryExpression;
                var ex = uex.Operand as MemberExpression;
                while (ex.Expression is MemberExpression)
                {
                    ex = ex.Expression as MemberExpression;
                }
                var cEx = ex.Expression as ConstantExpression;

                parentObject = cEx.Value;

                if (memberPath.Length > 2)   //In case path contains Member, parent object, parent object's parent etc. E.g. if the expression is in the form of () => Project.Zone.Floor.Width.
                {
                    int counter = memberPath.Length - 2;
                    while (counter > 0)
                    {
                        parentObject = parentObject.GetType().InvokeMember(memberPath[counter], BindingFlags.GetField | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, parentObject, null);
                        counter--;
                    }
                }

                owner = parentObject;
            }

            return owner;
        }

        private static string[] GetMemberPath(Expression expression)
        {
            List<string> path = new List<string>();

            if (expression is MemberExpression)
            {
                MemberExpression ex = expression as MemberExpression;
                path.Add(ex.Member.Name);
                while (ex.Expression is MemberExpression)
                {
                    ex = ex.Expression as MemberExpression;
                    path.Add(ex.Member.Name);
                }

                ConstantExpression cEx = ex.Expression as ConstantExpression;
                path.Add(cEx.Type.Name);
            }
            else if (expression is UnaryExpression)
            {
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
            }

            return path.ToArray();
        }

        #endregion
    }
}
