﻿using ReframeCore.Exceptions;
using ReframeCore.Helpers;
using ReframeCore.Nodes;
using ReframeCore.ReactiveCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Nodes
{
    public class CollectionPropertyNode : Node
    {
        private string _updateAllMethodName = "UpdateAll";

        public string UpdateMethodName { get; private set; }

        #region Constructors

        /// <summary>
        /// Instantiates new reactive node.
        /// </summary>
        /// <param name="collection">Collection of objects associated with the reactive node.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        public CollectionPropertyNode(object collection, string memberName)
            : base(collection, memberName)
        {
            ValidateArguments(collection, memberName);
        }

        /// <summary>
        /// Instantiates new reactive node.
        /// </summary>
        /// <param name="collection">Collection of objects associated with the reactive node.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        /// <param name="updateMethodName">Update method name.</param>
        public CollectionPropertyNode(object collection, string memberName, string updateMethodName)
            :base(collection, memberName)
        {
            ValidateArguments(collection, memberName, updateMethodName);
            UpdateMethod = Reflector.CreateAction(this, _updateAllMethodName);
            UpdateMethodName = updateMethodName;
        }

        /// <summary>
        /// Initializes reactive node's properties.
        /// </summary>
        /// <param name="ownerObject">Associated object which owns the member.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        /// <param name="updateMethod">Delegate to the update method.</param>
        protected override void Initialize(object collection, string memberName)
        {
            Predecessors = new List<INode>();
            Successors = new List<INode>();

            MemberName = memberName;
            OwnerObject = collection;

            Identifier = GetIdentifier();
        }

        #endregion

        #region Methods

        private void ValidateArguments(object ownerObject, string memberName)
        {
            if (Reflector.ContainsMember(ownerObject, memberName) == false)
            {
                throw new ReactiveNodeException("Unable to create reactive node! Not all provided arguments were valid!");
            }
        }

        protected void ValidateArguments(object ownerObject, string memberName, string updateMethodName)
        {
            ValidateArguments(ownerObject, memberName);

            if (updateMethodName != "" && Reflector.IsMethod(ownerObject, updateMethodName) == false)
            {
                throw new ReactiveNodeException("Unable to create reactive node! Provided update method is not a valid method!");
            }
        }

        private void UpdateAll()
        {
            if (UpdateMethodName != "")
            {
                IEnumerable collection = OwnerObject as IEnumerable;

                Action updateMethod;
                foreach (var obj in collection)
                {
                    updateMethod = Reflector.CreateAction(obj, UpdateMethodName);
                    updateMethod.Invoke();
                }
            }
        }

        #endregion
    }
}