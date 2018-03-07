using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Core.Operators;
using QL.Core.Types;

namespace QL.Core.Test.Operators
{
    [TestClass]
    public class OperatorResultTypeResolverTests
    {
        [TestMethod]
        public void IntegerAndMoney_WillPromoteToMoney()
        {
            Assert.AreEqual(QLType.Money, OperatorResultTypeResolver.ResolveOperationType(QLType.Integer, QLType.Money));
        }
    }
}
