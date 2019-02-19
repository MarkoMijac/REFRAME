﻿using ReframeCore.Exceptions;
using ReframeCore.Helpers;
using ReframeCore.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReframeCore.Nodes
{
    /// <summary>
    /// Reactive node which encapsulates object's property that needs to be tracked.
    /// </summary>
    public class PropertyNode : Node
    {
        #region Constructors

        /// <summary>
        /// Instantiates new reactive node.
        /// </summary>
        /// <param name="ownerObject">Associated object which owns the member.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        public PropertyNode(object ownerObject, string memberName) 
            :base(ownerObject, memberName)
        {
            
        }

        /// <summary>
        /// Instantiates new reactive node.
        /// </summary>
        /// <param name="ownerObject">Associated object which owns the member.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        /// <param name="updateMethodName">Update method name.</param>
        public PropertyNode(object ownerObject, string memberName, string updateMethodName)
            :base(ownerObject, memberName, updateMethodName)
        {
           
        }

        #endregion

        #region Methods

        /// <summary>
        /// Validates arguments passed in order to create reactive node.
        /// </summary>
        /// <param name="ownerObject">Associated object which owns the member.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        protected override void ValidateArguments(object ownerObject, string memberName, string updateMethodName)
        {
            if (ownerObject == null)
            {
                throw new ReactiveNodeException("Unable to create reactive node! Provided object is not valid!");
            }
            else if (Reflector.IsProperty(ownerObject, memberName) == false)
            {
                throw new ReactiveNodeException("Unable to create reactive node! Provided member is not a valid property!");
            }
            else if (updateMethodName != "" && Reflector.IsMethod(ownerObject, updateMethodName) == false)
            {
                throw new ReactiveNodeException("Unable to create reactive node! Provided update method is not a valid method!");
            }
        }

        #endregion
    }
}