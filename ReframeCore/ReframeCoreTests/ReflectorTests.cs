using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCoreTests.Helpers;
using System.Reflection;
using ReframeCore.Helpers;
using ReframeCoreExamples.E07_1;
using ReframeCoreExamples.E07;
using ReframeCore.ReactiveCollections;
using ReframeCoreExamples.E08.E1;
using ReframeCore.Exceptions;

namespace ReframeCoreTests
{
    [TestClass]
    public class ReflectorTests
    {
        private string _validMemberMethodName = "MemberMethodExample";
        private string _invalidMemberMethodName = "NonexistantMethod";
        private string _validMemberPropertyName = "MemberProperty";
        private string _invalidMemberPropertyName = "NonexistantProperty";

        [TestMethod]
        public void GetMethodInfo_NullInstance_ReturnsNull()
        {
            //ARRANGE
            ExampleClass ex = null;

            //ACT
            MethodInfo info = Reflector.GetMethodInfo(ex, _validMemberMethodName);

            //ASSERT
            Assert.IsNull(info);
        }

        [TestMethod]
        public void GetMethodInfo_EmptyMethodName_ReturnsNull()
        {
            //ARRANGE
            ExampleClass ex = new ExampleClass();

            //ACT
            MethodInfo info = Reflector.GetMethodInfo(ex, "");

            //ASSERT
            Assert.IsNull(info);
        }

        [TestMethod]
        public void GetMethodInfo_NonexistantMethodName_ReturnsNull()
        {
            //ARRANGE
            ExampleClass ex = new ExampleClass();

            //ACT
            MethodInfo info = Reflector.GetMethodInfo(ex, _invalidMemberMethodName);

            //ASSERT
            Assert.IsNull(info);
        }

        [TestMethod]
        public void GetMethodInfo_ValidInstanceAndMemberMethodName_ReturnsAppropriateMethodInfo()
        {
            //ARRANGE
            ExampleClass ex = new ExampleClass();

            //ACT
            MethodInfo info = Reflector.GetMethodInfo(ex, _validMemberMethodName);

            //ASSERT
            Assert.IsTrue(info.DeclaringType == typeof(ExampleClass) && info.Name == _validMemberMethodName);
        }

        [TestMethod]
        public void GetPropertyInfo_NullInstance_ReturnsNull()
        {
            //ARRANGE
            ExampleClass ex = null;

            //ACT
            PropertyInfo info = Reflector.GetPropertyInfo(ex, _validMemberPropertyName);

            //ASSERT
            Assert.IsNull(info);
        }

        [TestMethod]
        public void GetPropertyInfo_EmptyPropertyName_ReturnsNull()
        {
            //ARRANGE
            ExampleClass ex = new ExampleClass();

            //ACT
            PropertyInfo info = Reflector.GetPropertyInfo(ex, "");

            //ASSERT
            Assert.IsNull(info);
        }

        [TestMethod]
        public void GetPropertyInfo_NonexistantPropertyName_ReturnsNull()
        {
            //ARRANGE
            ExampleClass ex = new ExampleClass();

            //ACT
            PropertyInfo info = Reflector.GetPropertyInfo(ex, _invalidMemberPropertyName);

            //ASSERT
            Assert.IsNull(info);
        }

        [TestMethod]
        public void GetPropertyInfo_ValidInstanceAndMemberPropertyName_ReturnsAppropriatePropertyInfo()
        {
            //ARRANGE
            ExampleClass ex = new ExampleClass();

            //ACT
            PropertyInfo info = Reflector.GetPropertyInfo(ex, _validMemberPropertyName);

            //ASSERT
            Assert.IsTrue(info.DeclaringType == typeof(ExampleClass) && info.Name == _validMemberPropertyName);
        }

        [TestMethod]
        public void ContainsMember_NullInstance_ReturnsFalse()
        {
            //ARRANGE
            ExampleClass ex = null;

            //ACT
            bool contains = Reflector.ContainsMember(ex, _validMemberPropertyName);

            //ASSERT
            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void ContainsMember_EmptyMemberName_ReturnsFalse()
        {
            //ARRANGE
            ExampleClass ex = new ExampleClass();

            //ACT
            bool contains = Reflector.ContainsMember(ex, "");

            //ASSERT
            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void ContainsMember_NonexistantMemberName_ReturnsFalse()
        {
            //ARRANGE
            ExampleClass ex = new ExampleClass();

            //ACT
            bool contains = Reflector.ContainsMember(ex, _invalidMemberPropertyName);

            //ASSERT
            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void ContainsMember_ValidInstanceAndMemberPropertyName_ReturnsTrue()
        {
            //ARRANGE
            ExampleClass ex = new ExampleClass();

            //ACT
            bool contains = Reflector.ContainsMember(ex, _validMemberPropertyName);

            //ASSERT
            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void ContainsMember_ValidInstanceAndMemberMethodName_ReturnsTrue()
        {
            //ARRANGE
            ExampleClass ex = new ExampleClass();

            //ACT
            bool contains = Reflector.ContainsMember(ex, _validMemberMethodName);

            //ASSERT
            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void CreateAction_NullInstance_ReturnsNull()
        {
            //ARRANGE
            ExampleClass ex = new ExampleClass();
            MethodInfo info = Reflector.GetMethodInfo(ex, _validMemberMethodName);

            ExampleClass nullEx = null;

            //ACT
            Action action = Reflector.CreateAction(nullEx, info); 

            //ASSERT
            Assert.IsNull(action);
        }

        [TestMethod]
        public void CreateAction_NullMethodInfo_ReturnsNull()
        {
            //ARRANGE
            ExampleClass ex = new ExampleClass();
            MethodInfo info = null;

            //ACT
            Action action = Reflector.CreateAction(ex, info);

            //ASSERT
            Assert.IsNull(action);
        }

        [TestMethod]
        public void CreateAction_ValidInstanceAndMethodInfo_ReturnsAction()
        {
            //ARRANGE
            ExampleClass ex = new ExampleClass();
            MethodInfo info = Reflector.GetMethodInfo(ex, _validMemberMethodName);

            //ACT
            Action action = Reflector.CreateAction(ex, info);

            //ASSERT
            Assert.IsInstanceOfType(action, typeof(Action));
        }

        [TestMethod]
        public void CreateAction_ValidInstanceAndMethodInfo_ReturnsAppropriateAction()
        {
            //ARRANGE
            ExampleClass ex = new ExampleClass();
            MethodInfo info = Reflector.GetMethodInfo(ex, _validMemberMethodName);

            //ACT
            Action action = Reflector.CreateAction(ex, info);

            //ASSERT
            Assert.IsTrue(action.Target == ex && action.Method == info);
        }

        #region IsProperty(object obj, string memberName)

        [TestMethod]
        public void IsProperty_GivenNullObject_ReturnsFalse()
        {
            //ARRANGE
            Whole obj = null;
            string propertyName = "A";

            //ACT
            bool isProperty = Reflector.IsProperty(obj, propertyName);

            //ASSERT
            Assert.IsFalse(isProperty);
        }

        [TestMethod]
        public void IsProperty_GivenCorrectObjectAndNonExistingProperty_ReturnsFalse()
        {
            //ARRANGE
            Whole obj = new Whole();
            string propertyName = "Update_ABC";

            //ACT
            bool isProperty = Reflector.IsProperty(obj, propertyName);

            //ASSERT
            Assert.IsFalse(isProperty);
        }

        [TestMethod]
        public void IsProperty_GivenCorrectObjectAndMethodInsteadOfProperty_ReturnsFalse()
        {
            //ARRANGE
            Whole obj = new Whole();
            string propertyName = "Update_A";

            //ACT
            bool isProperty = Reflector.IsProperty(obj, propertyName);

            //ASSERT
            Assert.IsFalse(isProperty);
        }

        [TestMethod]
        public void IsProperty_GivenCorrectObjectAndExistingProperty_ReturnsTrue()
        {
            //ARRANGE
            Whole obj = new Whole();
            string propertyName = "A";

            //ACT
            bool isProperty = Reflector.IsProperty(obj, propertyName);

            //ASSERT
            Assert.IsTrue(isProperty);
        }

        [TestMethod]
        public void IsProperty_GivenCorrectCollectionObjectAndNonExistingProperty_ReturnsFalse()
        {
            //ARRANGE
            Whole obj = new Whole();
            string propertyName = "ABC";

            //ACT
            bool isProperty = Reflector.IsMethod(obj.Parts, propertyName);

            //ASSERT
            Assert.IsFalse(isProperty);
        }

        [TestMethod]
        public void IsProperty_GivenEmptyCollectionObjectAndExistingProperty_ReturnsTrue()
        {
            //ARRANGE
            ReactiveCollection<Part> parts = new ReactiveCollection<Part>();
            string propertyName = "A";

            //ACT
            bool isProperty = Reflector.IsProperty(parts, propertyName);

            //ASSERT
            Assert.IsTrue(isProperty);
        }

        [TestMethod]
        public void IsProperty_GivenCorrectCollectionObjectAndExistingProperty_ReturnsTrue()
        {
            //ARRANGE
            Whole obj = new Whole();
            string propertyName = "A";

            //ACT
            bool isProperty = Reflector.IsProperty(obj.Parts, propertyName);

            //ASSERT
            Assert.IsTrue(isProperty);
        }

        #endregion

        #region IsMethod(object obj, string memberName)

        [TestMethod]
        public void IsMethod_GivenNullObject_ReturnsFalse()
        {
            //ARRANGE
            Whole obj = null;
            string methodName = "Update_A";

            //ACT
            bool isMethod = Reflector.IsMethod(obj, methodName);

            //ASSERT
            Assert.IsFalse(isMethod);
        }

        [TestMethod]
        public void IsMethod_GivenCorrectObjectAndNonExistingMethod_ReturnsFalse()
        {
            //ARRANGE
            Whole obj = new Whole();
            string methodName = "Update_ABC";

            //ACT
            bool isMethod = Reflector.IsMethod(obj, methodName);

            //ASSERT
            Assert.IsFalse(isMethod);
        }

        [TestMethod]
        public void IsMethod_GivenCorrectObjectAndPropertyInsteadOfMethod_ReturnsFalse()
        {
            //ARRANGE
            Whole obj = new Whole();
            string methodName = "A";

            //ACT
            bool isMethod = Reflector.IsMethod(obj, methodName);

            //ASSERT
            Assert.IsFalse(isMethod);
        }

        [TestMethod]
        public void IsMethod_GivenCorrectObjectAndExistingMethod_ReturnsTrue()
        {
            //ARRANGE
            Whole obj = new Whole();
            string methodName = "Update_A";

            //ACT
            bool isMethod = Reflector.IsMethod(obj, methodName);

            //ASSERT
            Assert.IsTrue(isMethod);
        }

        [TestMethod]
        public void IsMethod_GivenCorrectCollectionObjectAndNonExistingMethod_ReturnsFalse()
        {
            //ARRANGE
            Whole obj = new Whole();
            string methodName = "Update_ABC";

            //ACT
            bool isMethod = Reflector.IsMethod(obj.Parts, methodName);

            //ASSERT
            Assert.IsFalse(isMethod);
        }

        [TestMethod]
        public void IsMethod_GivenCorrectCollectionObjectAndExistingMethod_ReturnsTrue()
        {
            //ARRANGE
            Whole obj = new Whole();
            string methodName = "Update_A";

            //ACT
            bool isMethod = Reflector.IsMethod(obj.Parts, methodName);

            //ASSERT
            Assert.IsTrue(isMethod);
        }

        [TestMethod]
        public void IsMethod_GivenEmptyCollectionObjectAndExistingMethod_ReturnsTrue()
        {
            //ARRANGE
            ReactiveCollection<Part> parts = new ReactiveCollection<Part>();
            string methodName = "Update_A";

            //ACT
            bool isMethod = Reflector.IsMethod(parts, methodName);

            //ASSERT
            Assert.IsTrue(isMethod);
        }

        #endregion

        #region  IsGenericCollection(object obj)

        [TestMethod]
        public void IsGenericCollection_GivenNullCollection_ReturnsFalse()
        {
            //ARRANGE
            ReactiveCollection<Part> parts = null;

            //ACT
            bool isGenericCollection = Reflector.IsGenericCollection(parts);

            //ASSERT
            Assert.IsFalse(isGenericCollection);
        }

        [TestMethod]
        public void IsGenericCollection_GivenNonCollectionObject_ReturnsFalse()
        {
            //ARRANGE
            Part part = new Part();

            //ACT
            bool isGenericCollection = Reflector.IsGenericCollection(part);

            //ASSERT
            Assert.IsFalse(isGenericCollection);
        }

        [TestMethod]
        public void IsGenericCollection_GivenEmptyCollectionObject_ReturnsTrue()
        {
            //ARRANGE
            ReactiveCollection<Part> parts = new ReactiveCollection<Part>();

            //ACT
            bool isGenericCollection = Reflector.IsGenericCollection(parts);

            //ASSERT
            Assert.IsTrue(isGenericCollection);
        }

        [TestMethod]
        public void IsGenericCollection_GivenCollectionObject_ReturnsTrue()
        {
            //ARRANGE
            Whole whole = new Whole();

            //ACT
            bool isGenericCollection = Reflector.IsGenericCollection(whole.Parts);

            //ASSERT
            Assert.IsTrue(isGenericCollection);
        }

        #endregion

        #region RaiseEvent

        [TestMethod]
        public void RaiseEvent_GivenValidEvent_RaisesEvent()
        {
            //Arrange
            Part_8_1 part = new Part_8_1();
            bool eventRaised = false;
            part.UpdateTriggered += delegate (object sender, EventArgs e)
            {
                eventRaised = true;
            };

            ReactiveCollectionItemEventArgs eArgs = new ReactiveCollectionItemEventArgs();
            eArgs.MemberName = "A";

            //Act
            Reflector.RaiseEvent(part, "UpdateTriggered", eArgs);

            //Assert
            Assert.IsTrue(eventRaised);
        }

        [TestMethod]
        public void RaiseEvent_GivenNullObject_DoesNotRaiseEvent()
        {
            //Arrange
            Part_8_1 part = new Part_8_1();
            part.UpdateTriggered += delegate (object sender, EventArgs e) { };

            ReactiveCollectionItemEventArgs eArgs = new ReactiveCollectionItemEventArgs();
            eArgs.MemberName = "A";

            //Act&Assert
            Assert.ThrowsException<ReflectorException>(() => Reflector.RaiseEvent(null, "UpdateTriggered_Inv", eArgs));
        }

        [TestMethod]
        public void RaiseEvent_GivenInvalidEvent_DoesNotRaiseEvent()
        {
            //Arrange
            Part_8_1 part = new Part_8_1();
            part.UpdateTriggered += delegate (object sender, EventArgs e) { };

            ReactiveCollectionItemEventArgs eArgs = new ReactiveCollectionItemEventArgs();
            eArgs.MemberName = "A";

            //Act&Assert
            Assert.ThrowsException<ReflectorException>(() => Reflector.RaiseEvent(part, "UpdateTriggered_Inv", eArgs));
        }

        #endregion
    }
}
