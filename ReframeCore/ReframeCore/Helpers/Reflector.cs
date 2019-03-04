using ReframeCore.Exceptions;
using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Helpers
{
    /// <summary>
    /// Provider of reflection helper methods.
    /// </summary>
    public static class Reflector
    {
        /// <summary>
        /// Gets info about method.
        /// </summary>
        /// <param name="obj">Object which contains the method.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <returns>Info about the method.</returns>
        public static MethodInfo GetMethodInfo(object obj, string methodName)
        {
            MethodInfo methodInfo = null;

            if (obj != null && methodName != "")
            {
                methodInfo = obj.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            }

            return methodInfo;
        }

        /// <summary>
        /// Gets info about property.
        /// </summary>
        /// <param name="obj">Object which contains the property.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Info about the property.</returns>
        public static PropertyInfo GetPropertyInfo(object obj, string propertyName)
        {
            PropertyInfo propertyInfo = null;

            if (obj != null && propertyName != "")
            {
                propertyInfo = obj.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            }

            return propertyInfo;
        }

        /// <summary>
        /// Checks if object contains member with specified name.
        /// </summary>
        /// <param name="obj">Object which contains the member.</param>
        /// <param name="memberName">Name of the member.</param>
        /// <returns>True if object contains member with specified name, otherwise False.</returns>
        public static bool ContainsMember(object obj, string memberName)
        {
            bool contains = false;

            if (obj != null && memberName != "")
            {
                Type type;

                if (IsGenericCollection(obj) == true)
                {
                    type = obj.GetType().GenericTypeArguments[0];
                }
                else
                {
                    type = obj.GetType();
                }

                contains = ContainsMember(type, memberName);
            }

            return contains;
        }

        private static Type GetGenericArgumentType(object obj)
        {
            Type type = null;

            if (IsGenericCollection(obj) == true)
            {
                type = obj.GetType().GenericTypeArguments[0];
            }

            return type;
        }

        private static bool ContainsMember(Type type, string memberName)
        {
            bool contains = false;

            MemberInfo[] infos = type.GetMember(memberName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (infos.Length > 0)
            {
                contains = true;
            }

            return contains;
        }

        /// <summary>
        /// Creates action delegate referencing specified method of the object.
        /// </summary>
        /// <param name="obj">Object which contains the method.</param>
        /// <param name="methodInfo">Method to be referenced by delegate.</param>
        /// <returns>Action delegate referencing specified method of the object.</returns>
        public static Action CreateAction(object obj, MethodInfo methodInfo)
        {
            Action action = null;

            if (obj != null && methodInfo != null)
            {
                action = (Action)Delegate.CreateDelegate(typeof(Action), obj, methodInfo);
            }

            return action;
        }

        /// <summary>
        /// Creates action delegate referencing specified method of the object.
        /// </summary>
        /// <param name="obj">Object which contains the method.</param>
        /// <param name="methodName">Method to be referenced by delegate.</param>
        /// <returns>Action delegate referencing specified method of the object.</returns>
        public static Action CreateAction(object obj, string methodName)
        {
            MethodInfo methodInfo = GetMethodInfo(obj, methodName);
            return CreateAction(obj, methodInfo);
        }

        /// <summary>
        /// Checks if specified member is a property.
        /// </summary>
        /// <param name="obj">Object which contains the member.</param>
        /// <param name="memberName">Name of the member.</param>
        /// <returns>True if the member is a property, otherwise False.</returns>
        public static bool IsProperty(object obj, string memberName)
        {
            bool isProperty = false;

            if (obj != null && memberName != "")
            {
                Type type;
                if (IsGenericCollection(obj) == true)
                {
                    type = GetGenericArgumentType(obj);
                }
                else
                {
                    type = obj.GetType();
                }

                isProperty = IsProperty(type, memberName);
            }

            return isProperty;
        }

        private static bool IsProperty(Type type, string memberName)
        {
            bool isProperty = false;

            if (type != null && memberName != "")
            {
                MemberInfo[] infos = type.GetMember(memberName, MemberTypes.Property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                if (infos.Length > 0)
                {
                    isProperty = true;
                }
            }

            return isProperty;
        }

        /// <summary>
        /// Checks if specified member is a method.
        /// </summary>
        /// <param name="obj">Object which contains the member.</param>
        /// <param name="memberName">Name of the member.</param>
        /// <returns>True if the member is a method, otherwise False.</returns>
        public static bool IsMethod(object obj, string memberName)
        {
            bool isMethod = false;

            if (obj != null && memberName != "")
            {
                Type type;
                if (IsGenericCollection(obj) == true)
                {
                    type = GetGenericArgumentType(obj);
                }
                else
                {
                    type = obj.GetType();
                }

                isMethod = IsMethod(type, memberName);
            }

            return isMethod;
        }

        /// <summary>
        /// Checks if specified member is a method.
        /// </summary>
        /// <param name="type">Type which specifies the member.</param>
        /// <param name="memberName">Name of the member.</param>
        /// <returns>True if the member is a method, otherwise False.</returns>
        private static bool IsMethod(Type type, string memberName)
        {
            bool isMethod = false;

            if (type != null && memberName != "")
            {
                MemberInfo[] infos = type.GetMember(memberName, MemberTypes.Method, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (infos.Length > 0)
                {
                    isMethod = true;
                }
            }

            return isMethod;
        }

        /// <summary>
        /// Checks if specified object is a generic reactive collection.
        /// </summary>
        /// <param name="obj">Possible reactive collection.</param>
        /// <returns>True if the specified object is reactive collection, otherwise False.</returns>
        public static bool IsGenericCollection(object obj)
        {
            bool isColl = false;

            if (obj != null)
            {
                Type t = obj.GetType();
                if (t.IsGenericType == true && t.GetGenericTypeDefinition() == typeof(ReactiveCollection<>))
                {
                    isColl = true;
                }
            }

            return isColl;
        }

        private static MulticastDelegate GetMulticastDelegate(object obj, string eventName)
        {
            MulticastDelegate multicastDelegate = null;

            try
            {
                multicastDelegate = (MulticastDelegate)obj.GetType().GetField(eventName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetValue(obj);
            }
            catch (Exception)
            {
                throw new ReflectorException("No delegate could be obtained for provided arguments!");
            }

            return multicastDelegate;
        }

        public static void RaiseEvent(object obj, string eventName, EventArgs e)
        {
            MulticastDelegate multicastDelegate = GetMulticastDelegate(obj, eventName);

            if (multicastDelegate != null)
            {
                foreach (var del in multicastDelegate.GetInvocationList())
                {
                    del.Method.Invoke(del.Target, new object[] { obj, e });
                }
            }
        }
    }
}
