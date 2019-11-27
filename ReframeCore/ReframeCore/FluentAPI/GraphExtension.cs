using ReframeCore.Exceptions;
using ReframeCore.Nodes;
using ReframeCore.ReactiveCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ReframeCore.Factories;
using ReframeCore.Helpers;

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

        public static TransferObject Let(this IDependencyGraph instance, IReactiveCollection collection, Expression<Func<object>> expression)
        {
            List<INode> addedNodes = new List<INode>();
            ValidateGraphInstance(instance);
            ValidateReactiveCollection(collection);

            string memberName = MemberReader.GetMemberName(expression);
            ValidateMemberName(memberName);
            INode successor = instance.AddNode(collection, memberName);

            addedNodes.Add(successor);
            TransferObject to = new TransferObject(instance, addedNodes);

            return to;
        }

        public static TransferObject Let(this IDependencyGraph instance, IReactiveCollection collection, Expression<Action> expression)
        {
            List<INode> addedNodes = new List<INode>();
            ValidateGraphInstance(instance);
            ValidateReactiveCollection(collection);

            string memberName = MemberReader.GetMemberName(expression);
            ValidateMemberName(memberName);
            INode successor = instance.AddNode(collection, memberName);

            addedNodes.Add(successor);
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

        public static void DependOn(this TransferObject instance, params Expression<Func<object>>[] expressions)
        {
            TransferObject transferObject = instance;
            ValidateTransferObject(transferObject);

            string memberName;
            object ownerObject;
            INode predecessor;

            foreach (var successor in transferObject.Successors)
            {
                foreach (var expression in expressions)
                {
                    ownerObject = MemberReader.GetMemberOwner(expression);
                    ValidateOwnerObject(ownerObject);
                    memberName = MemberReader.GetMemberName(expression);
                    ValidateMemberName(memberName);

                    predecessor = transferObject.Graph.AddNode(ownerObject, memberName);
                    transferObject.Graph.AddDependency(predecessor, successor);
                }
            }
        }

        public static void DependOn(this TransferObject instance, params Expression<Action>[] expressions)
        {
            TransferObject transferObject = instance;
            ValidateTransferObject(transferObject);

            string memberName;
            object ownerObject;
            INode predecessor;

            foreach (var successor in transferObject.Successors)
            {
                foreach (var expression in expressions)
                {
                    ownerObject = MemberReader.GetMemberOwner(expression);
                    ValidateOwnerObject(ownerObject);
                    memberName = MemberReader.GetMemberName(expression);
                    ValidateMemberName(memberName);

                    predecessor = transferObject.Graph.AddNode(ownerObject, memberName);
                    transferObject.Graph.AddDependency(predecessor, successor);
                }
            }
        }

        public static void DependOn(this TransferObject instance, IReactiveCollection collection, Expression<Func<object>> expression)
        {
            TransferObject transferObject = instance;

            ValidateTransferObject(transferObject);
            ValidateReactiveCollection(collection);

            string memberName = MemberReader.GetMemberName(expression);
            ValidateMemberName(memberName);
            INode predecessor = transferObject.Graph.AddNode(collection, memberName);

            foreach (var successor in transferObject.Successors)
            {
                transferObject.Graph.AddDependency(predecessor, successor);
            }
        }

        public static void DependOn(this TransferObject instance, IReactiveCollection collection, Expression<Action> expression)
        {
            TransferObject transferObject = instance;

            ValidateTransferObject(transferObject);
            ValidateReactiveCollection(collection);

            string memberName = MemberReader.GetMemberName(expression);
            ValidateMemberName(memberName);
            INode predecessor = transferObject.Graph.AddNode(collection, memberName);

            foreach (var successor in transferObject.Successors)
            {
                transferObject.Graph.AddDependency(predecessor, successor);
            }
        }

        private static void ValidateTransferObject(TransferObject transferObject)
        {
            if (transferObject == null)
            {
                throw new FluentException("Transfer object cannot be null!");
            }
            else if (transferObject.Graph == null)
            {
                throw new FluentException("Dependency graph cannot be null!");
            }
            else if (transferObject.Successors == null)
            {
                throw new FluentException("List of successor nodes cannot be null!");
            }
            else if (transferObject.Successors.Count == 0)
            {
                throw new FluentException("List of successor nodes cannot be empty!");
            }
        }

        private static void ValidateReactiveCollection(IReactiveCollection collection)
        {
            if (collection == null)
            {
                throw new FluentException("Reactive collection object cannot be null!");
            }
        }

        public static void Update(this object instance, Updater updater, [CallerMemberNameAttribute] string memberName = "")
        {
            if (updater == null)
            {
                throw new UpdaterException("Updater is null!");
            }

            if (instance == null)
            {
                throw new NodeNullReferenceException("Owner object is null!");
            }
            updater.PerformUpdate(instance, memberName);
        }
    }
}
