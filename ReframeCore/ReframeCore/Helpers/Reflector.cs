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
    }
}
