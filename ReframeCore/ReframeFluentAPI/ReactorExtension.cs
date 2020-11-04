using ReframeCore;
using ReframeCore.Exceptions;
using ReframeCore.Factories;
using ReframeCore.Helpers;
using ReframeCore.Nodes;
using ReframeCore.ReactiveCollections;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace ReframeCoreFluentAPI
{
    public static class ReactorExtension
    {
        #region Validate

        private static void ValidateReactor(IReactor reactor)
        {
            if (reactor == null)
            {
                throw new FluentException("Reactor must be set!");
            }
        }

        private static void ValidateOwner(object owner)
        {
            if (owner == null)
            {
                throw new FluentException("Owner must be set!");
            }

            Type type = owner.GetType();
            var attr = type.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false);
            if (attr != null && attr.Length > 0)
            {
                throw new FluentException("Owner must be set!");
            }
        }

        private static void ValidateMemberName(string memberName)
        {
            if (memberName == "")
            {
                throw new FluentException("Member name cannot be empty!");
            }
        }

        private static void ValidateReactiveCollection(IReactiveCollection collection)
        {
            if (collection == null)
            {
                throw new FluentException("Reactive collection object cannot be null!");
            }
        }

        private static void ValidateTransferObject(TransferParameter transferParameter)
        {
            if (transferParameter == null)
            {
                throw new FluentException("Transfer object cannot be null!");
            }
            else if (transferParameter.Reactor == null)
            {
                throw new FluentException("Reactor cannot be null!");
            }
            else if (transferParameter.Successors == null)
            {
                throw new FluentException("List of successor nodes cannot be null!");
            }
            else if (transferParameter.Successors.Count == 0)
            {
                throw new FluentException("List of successor nodes cannot be empty!");
            }
        }

        #endregion

        #region Let

        public static TransferParameter Let(this IReactor instance, params Expression<Action>[] expressions)
        {
            List<INode> addedNodes = new List<INode>();

            ValidateReactor(instance);

            foreach (var expression in expressions)
            {
                object owner = MemberReader.GetMemberOwner(expression);
                ValidateOwner(owner);

                string member = MemberReader.GetMemberName(expression);
                ValidateMemberName(member);

                INode n = instance.AddNode(owner, member);
                addedNodes.Add(n);
            }

            TransferParameter to = new TransferParameter(instance, addedNodes);

            return to;
        }

        public static TransferParameter Let(this IReactor instance, params Expression<Func<object>>[] expressions)
        {
            List<INode> addedNodes = new List<INode>();

            ValidateReactor(instance);

            foreach (var expression in expressions)
            {
                object owner = MemberReader.GetMemberOwner(expression);
                ValidateOwner(owner);

                string member = MemberReader.GetMemberName(expression);
                ValidateMemberName(member);

                INode n = instance.AddNode(owner, member);
                addedNodes.Add(n);
            }

            TransferParameter to = new TransferParameter(instance, addedNodes);

            return to;
        }

        public static TransferParameter Let(this IReactor instance, IReactiveCollection collection, Expression<Func<object>> expression)
        {
            List<INode> addedNodes = new List<INode>();
            ValidateReactor(instance);
            ValidateReactiveCollection(collection);

            string memberName = MemberReader.GetMemberName(expression);
            ValidateMemberName(memberName);
            INode successor = instance.AddNode(collection, memberName);

            addedNodes.Add(successor);
            TransferParameter to = new TransferParameter(instance, addedNodes);

            return to;
        }

        public static TransferParameter Let(this IReactor instance, IReactiveCollection collection, Expression<Action> expression)
        {
            List<INode> addedNodes = new List<INode>();
            ValidateReactor(instance);
            ValidateReactiveCollection(collection);

            string memberName = MemberReader.GetMemberName(expression);
            ValidateMemberName(memberName);
            INode successor = instance.AddNode(collection, memberName);

            addedNodes.Add(successor);
            TransferParameter to = new TransferParameter(instance, addedNodes);

            return to;
        }

        #endregion

        #region DependOn

        public static void DependOn(this TransferParameter instance, params Expression<Func<object>>[] expressions)
        {
            TransferParameter transferParameter = instance;
            ValidateTransferObject(transferParameter);

            string memberName;
            object ownerObject;
            INode predecessor;

            foreach (var successor in transferParameter.Successors)
            {
                foreach (var expression in expressions)
                {
                    ownerObject = MemberReader.GetMemberOwner(expression);
                    ValidateOwner(ownerObject);
                    memberName = MemberReader.GetMemberName(expression);
                    ValidateMemberName(memberName);

                    predecessor = transferParameter.Reactor.AddNode(ownerObject, memberName);
                    transferParameter.Reactor.AddDependency(predecessor, successor);
                }
            }
        }

        public static void DependOn(this TransferParameter instance, params Expression<Action>[] expressions)
        {
            TransferParameter transferParameter = instance;
            ValidateTransferObject(transferParameter);

            string memberName;
            object ownerObject;
            INode predecessor;

            foreach (var successor in transferParameter.Successors)
            {
                foreach (var expression in expressions)
                {
                    ownerObject = MemberReader.GetMemberOwner(expression);
                    ValidateOwner(ownerObject);
                    memberName = MemberReader.GetMemberName(expression);
                    ValidateMemberName(memberName);

                    predecessor = transferParameter.Reactor.AddNode(ownerObject, memberName);
                    transferParameter.Reactor.AddDependency(predecessor, successor);
                }
            }
        }

        public static void DependOn(this TransferParameter instance, IReactiveCollection collection, Expression<Func<object>> expression)
        {
            TransferParameter transferObject = instance;

            ValidateTransferObject(transferObject);
            ValidateReactiveCollection(collection);

            string memberName = MemberReader.GetMemberName(expression);
            ValidateMemberName(memberName);
            INode predecessor = transferObject.Reactor.AddNode(collection, memberName);

            foreach (var successor in transferObject.Successors)
            {
                transferObject.Reactor.AddDependency(predecessor, successor);
            }
        }

        public static void DependOn(this TransferParameter instance, IReactiveCollection collection, Expression<Action> expression)
        {
            TransferParameter transferObject = instance;

            ValidateTransferObject(transferObject);
            ValidateReactiveCollection(collection);

            string memberName = MemberReader.GetMemberName(expression);
            ValidateMemberName(memberName);
            INode predecessor = transferObject.Reactor.AddNode(collection, memberName);

            foreach (var successor in transferObject.Successors)
            {
                transferObject.Reactor.AddDependency(predecessor, successor);
            }
        }

        #endregion

        #region Update

        public static void Update(this object instance, [CallerMemberNameAttribute] string memberName = "")
        {
            foreach (var reactor in ReactorRegistry.Instance.GetReactors())
            {
                INode node = reactor.GetNode(instance, memberName);
                if (node == null && instance is ICollectionNodeItem)
                {
                    node = GraphUtility.GetCollectionNode((ICollectionNodeItem)instance, memberName);
                }
                reactor.PerformUpdate(node);
            }
        }

        public static void Update(this IReactor instance, object ownerObject, [CallerMemberNameAttribute] string memberName = "")
        {
            if (instance == null)
            {
                throw new ReactorException("Reactor cannot be null!");
            }

            if (ownerObject == null)
            {
                throw new NodeNullReferenceException("Owner object is null!");
            }

            instance.PerformUpdate(ownerObject, memberName);
        }

        #endregion
    }
}
