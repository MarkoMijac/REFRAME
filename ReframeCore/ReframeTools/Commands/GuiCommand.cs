using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReframeTools.Commands
{
    internal class GuiCommand
    {
        public string Identifier { get; private set; }
        public string Text { get; set; }
        public EventHandler Handler { get; set; }

        public GuiCommand(string identifier, string text, object obj, string methodName)
        {
            Identifier = identifier;
            Text = text;

            var methodInfo = GetMethodInfo(obj, methodName);
            var action = CreateAction(obj, methodInfo);
            Handler = CreateHandler(action);
        }

        public void Invoke(object sender, EventArgs e)
        {
            Handler?.Invoke(sender, e);
        }

        private static MethodInfo GetMethodInfo(object obj, string methodName)
        {
            MethodInfo methodInfo = null;

            if (obj != null && methodName != "")
            {
                methodInfo = obj.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            }

            return methodInfo;
        }

        private static Action CreateAction(object obj, MethodInfo methodInfo)
        {
            Action action = null;

            if (obj != null && methodInfo != null)
            {
                action = (Action)Delegate.CreateDelegate(typeof(Action), obj, methodInfo);
            }

            return action;
        }

        private static EventHandler CreateHandler(object obj, MethodInfo methodInfo)
        {
            EventHandler handler = null;

            if (methodInfo != null)
            {
                if (methodInfo.IsStatic == true)
                {
                    handler = Delegate.CreateDelegate(typeof(EventHandler), methodInfo) as EventHandler;
                }
                else
                {
                    handler = Delegate.CreateDelegate(typeof(EventHandler), obj, methodInfo) as EventHandler;
                }
            }

            return handler;
        }

        private static EventHandler CreateHandler(Action action)
        {
            return delegate { action?.Invoke(); };
        }
    }
}
