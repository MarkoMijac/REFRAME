using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;
using ReframeCore.FluentAPI;
using ReframeCoreExamples.E00;
using System.Reflection;
using ReframeCore.Helpers;
using ReframeCoreExamples.E01;

namespace ReframeCoreTests
{
    [TestClass]
    public class MemberReaderTests
    {
        public int TestA { get; set; }

        #region GetMemberName

        [TestMethod]
        public void GetMemberName_GivenExpressionIsNull_ThrowsException()
        {
            //Arrange
            Expression<Func<object>> lambda = null;

            //Act&Assert
            Assert.ThrowsException<FluentException>(() =>MemberReader.GetMemberName(lambda));
        }

        [TestMethod]
        public void GetMemberName_GivenPropertyValueMember_ReturnsPropertyName()
        {
            //Arrange
            GenericReactiveObject3 obj = new GenericReactiveObject3();
            Expression<Func<object>> lambda = () => obj.A;

            //Act
            string memberName = MemberReader.GetMemberName(lambda);

            //Assert
            Assert.AreEqual("A", memberName);
        }

        [TestMethod]
        public void GetMemberName_GivenPropertyReferenceMember_ReturnsPropertyName()
        {
            //Arrange
            Apartment01 apartment = new Apartment01();
            Expression<Func<object>> lambda = () => apartment.Balcony;

            //Act
            string memberName = MemberReader.GetMemberName(lambda);

            //Assert
            Assert.AreEqual("Balcony", memberName);
        }

        [TestMethod]
        public void GetMemberName_GivenMethodMember_ReturnsMethodName()
        {
            //Arrange
            GenericReactiveObject3 obj = new GenericReactiveObject3();
            Expression<Action> lambda = () => obj.Update_A();

            //Act
            string memberName = MemberReader.GetMemberName(lambda);

            //Assert
            Assert.AreEqual("Update_A", memberName);
        }

        [TestMethod]
        public void GetMemberName_GivenNestedReferenceProperty_ReturnsPropertyName()
        {
            //Arrange
            GenericReactiveObject3 obj = new GenericReactiveObject3();
            Expression<Func<object>> lambda = () => obj.NestedObject.A;

            //Act
            string memberName = MemberReader.GetMemberName(lambda);

            //Assert
            Assert.AreEqual("A", memberName);
        }

        [TestMethod]
        public void GetMemberName_GivenNestedReferenceMethod_ReturnsMethodName()
        {
            //Arrange
            GenericReactiveObject3 obj = new GenericReactiveObject3();
            Expression<Func<object>> lambda = () => obj.NestedObject.ToString();

            //Act
            string memberName = MemberReader.GetMemberName(lambda);

            //Assert
            Assert.AreEqual("ToString", memberName);
        }

        #endregion

        #region GetMemberOwner

        [TestMethod]
        public void GetMemberOwner_GivenExpressionIsNull_ThrowsException()
        {
            //Arrange
            Expression<Func<object>> lambda = null;

            //Act&Assert
            Assert.ThrowsException<FluentException>(() => MemberReader.GetMemberOwner(lambda));
        }

        [TestMethod]
        public void GetMemberOwner_GivenPropertyMember_ReturnsOwner()
        {
            //Arrange
            GenericReactiveObject3 obj = new GenericReactiveObject3();
            Expression<Func<object>> lambda = () => obj.A;

            //Act
            object owner = MemberReader.GetMemberOwner(lambda);

            //Assert
            Assert.AreEqual(obj, owner);
        }

        [TestMethod]
        public void GetMemberName_GivenNestedObject_ReturnsOwner()
        {
            //Arrange
            GenericReactiveObject3 obj = new GenericReactiveObject3();
            Expression<Func<object>> lambda = () => obj.NestedObject.ToString();

            //Act
            object owner = MemberReader.GetMemberOwner(lambda);

            //Assert
            Assert.AreEqual(obj.NestedObject, owner);
        }

        [TestMethod]
        public void GetMemberName_GivenInScopeProperty_ReturnsOwner()
        {
            //Arrange
            GenericReactiveObject3 obj = new GenericReactiveObject3();
            Expression<Func<object>> lambda = () => this.TestA;

            //Act
            object owner = MemberReader.GetMemberOwner(lambda);

            //Assert
            Assert.AreEqual(this, owner);
        }

        #endregion
    }
}
