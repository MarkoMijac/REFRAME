using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReframeCoreTests.Helpers;
using System.Reflection;
using ReframeCore.Helpers;

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
    }
}
