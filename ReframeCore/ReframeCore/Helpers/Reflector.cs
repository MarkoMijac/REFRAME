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
                MemberInfo[] infos = obj.GetType().GetMember(memberName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (infos.Length > 0)
                {
                    contains = true;
                }
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
                MemberInfo[] infos = obj.GetType().GetMember(memberName, MemberTypes.Property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

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
                MemberInfo[] infos = obj.GetType().GetMember(memberName, MemberTypes.Method, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (infos.Length > 0)
                {
                    isMethod = true;
                }
            }

            return isMethod;
        }

        /// <summary>
        /// Gets the type of the member.
        /// </summary>
        /// <param name="obj">Object which contains the member.</param>
        /// <param name="memberName">Name of the member.</param>
        /// <returns>Type of the member.</returns>
        public static MemberTypes GetMemberType(object obj, string memberName)
        {
            MemberTypes type = default(MemberTypes);

            if (obj != null && memberName != "")
            {
                MemberInfo[] infos = obj.GetType().GetMember(memberName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (infos.Length > 0)
                {
                    type = infos[0].MemberType;
                }
            }

            return type;
        }
    }
}
