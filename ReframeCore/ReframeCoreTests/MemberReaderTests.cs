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
        [TestMethod]
        public void GetMemberName_GivenExpressionIsNull_ThrowsException()
        {
            //Arrange
            Expression ex = null;

            //Act&Assert
            Assert.ThrowsException<FluentException>(() =>MemberReader.GetMemberName(ex));
        }

        [TestMethod]
        public void GetMemberName_GivenPropertyValueMember_ReturnsPropertyName()
        {
            //Arrange
            GenericReactiveObject3 obj = new GenericReactiveObject3();
            Expression<Func<object>> lambda = () => obj.A;
            Expression expression = lambda.Body;

            //Act
            string memberName = MemberReader.GetMemberName(expression);

            //Assert
            Assert.AreEqual("A", memberName);
        }

        [TestMethod]
        public void GetMemberName_GivenPropertyReferenceMember_ReturnsPropertyName()
        {
            //Arrange
            Apartment01 apartment = new Apartment01();
            Expression<Func<object>> lambda = () => apartment.Balcony;
            Expression expression = lambda.Body;

            //Act
            string memberName = MemberReader.GetMemberName(expression);

            //Assert
            Assert.AreEqual("Balcony", memberName);
        }

        [TestMethod]
        public void GetMemberName_GivenMethodMember_ReturnsMethodName()
        {
            //Arrange
            GenericReactiveObject3 obj = new GenericReactiveObject3();
            Expression<Action> lambda = () => obj.Update_A();
            Expression expression = lambda.Body;

            //Act
            string memberName = MemberReader.GetMemberName(expression);

            //Assert
            Assert.AreEqual("Update_A", memberName);
        }
    }
}
