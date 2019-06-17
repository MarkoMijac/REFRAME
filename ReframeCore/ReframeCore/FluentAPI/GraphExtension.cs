using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.FluentAPI
{
    public static class GraphExtension
    {
        public static TransferObject Let(this IDependencyGraph instance, params Expression<Action>[] expressions)
        {
            object ownerObject = null;
            string memberName = "";
            List<INode> addedNodes = new List<INode>();

            ValidateGraphInstance(instance);

            foreach (var expression in expressions)
            {
                ownerObject = MemberReader.GetMemberOwner(expression);
                ValidateOwnerObject(ownerObject);

                memberName = MemberReader.GetMemberName(expression);
                ValidateMemberName(memberName);

                INode n = instance.AddNode(ownerObject, memberName);
                addedNodes.Add(n);
            }

            TransferObject to = new TransferObject(instance, addedNodes);

            return to;
        }

        public static TransferObject Let(this IDependencyGraph instance, params Expression<Func<object>>[] expressions)
        {
            object ownerObject = null;
            string memberName = "";
            List<INode> addedNodes = new List<INode>();

            ValidateGraphInstance(instance);

            foreach (var expression in expressions)
            {
                ownerObject = MemberReader.GetMemberOwner(expression);
                ValidateOwnerObject(ownerObject);

                memberName = MemberReader.GetMemberName(expression);
                ValidateMemberName(memberName);

                INode n = instance.AddNode(ownerObject, memberName);
                addedNodes.Add(n);
            }

            TransferObject to = new TransferObject(instance, addedNodes);

            return to;
        }

        private static void ValidateGraphInstance(IDependencyGraph instance)
        {
            if (instance == null)
            {
                throw new FluentException("Dependency graph cannot be null!");
            }
        }

        private static void ValidateOwnerObject(object ownerObject)
        {
            if (ownerObject == null)
            {
                throw new FluentException("Owner object cannot be null!");
            }

            Type type = ownerObject.GetType();
            var attr = type.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false);
            if (attr != null && attr.Length > 0)
            {
                throw new FluentException("Owner object cannot be null!");
            }
        }

        private static void ValidateMemberName(string memberName)
        {
            if (memberName == "")
            {
                throw new FluentException("Member name cannot be empty!");
            }
        }
    }
}
