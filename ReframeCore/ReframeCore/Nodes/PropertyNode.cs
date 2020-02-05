using ReframeCore.Exceptions;
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
        public object LastValue { get; private set; }
        private string _updateMethodName;

        #region Constructors

        /// <summary>
        /// Instantiates new reactive node.
        /// </summary>
        /// <param name="ownerObject">Associated object which owns the member.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        public PropertyNode(object ownerObject, string memberName) 
            :base(ownerObject, memberName)
        {
            LastValue = Reflector.GetPropertyValue(ownerObject, memberName);
        }

        /// <summary>
        /// Instantiates new reactive node.
        /// </summary>
        /// <param name="ownerObject">Associated object which owns the member.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        /// <param name="updateMethodName">Update method name.</param>
        public PropertyNode(object ownerObject, string memberName, string updateMethodName)
            :base(ownerObject, memberName)
        {
            IsValidUpdateMethod(ownerObject, updateMethodName);
            _updateMethodName = updateMethodName;
            LastValue = Reflector.GetPropertyValue(ownerObject, memberName);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Validates arguments passed in order to create reactive node.
        /// </summary>
        /// <param name="ownerObject">Associated object which owns the member.</param>
        /// <param name="memberName">The name of the class member reactive node represents.</param>
        private void IsValidUpdateMethod(object ownerObject, string updateMethodName)
        {
            if (updateMethodName != "" && Reflector.IsMethod(ownerObject, updateMethodName) == false)
            {
                throw new ReactiveNodeException("Unable to create reactive node! Provided update method is not a valid method!");
            }
        }

        public object GetCurrentValue()
        {
            return Reflector.GetPropertyValue(OwnerObject, MemberName);
        }

        public override bool IsTriggered()
        {
            object currentValue = GetCurrentValue();

            bool isChanged = currentValue.Equals(LastValue) == false;

            return isChanged;
        }

        protected override Action GetUpdateMethod()
        {
            Action action = null;

            if (OwnerObject != null && _updateMethodName!=null && _updateMethodName != "")
            {
                action = Reflector.CreateAction(OwnerObject, _updateMethodName);
            }

            return action;
        }

        protected override bool IsValidNode(object owner, string memberName, out string message)
        {
            message = "";
            if (Reflector.IsProperty(owner, memberName) == false)
            {
                message = "Unable to create reactive node! Provided member is not a valid property!";
                return false;
            }
            
            return true;
        }

        #endregion
    }
}
