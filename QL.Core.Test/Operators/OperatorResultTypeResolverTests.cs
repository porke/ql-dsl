using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Core.Operators;
using QL.Core.Types;

namespace QL.Core.Test.Operators
{
    [TestClass]
    public class OperatorResultTypeResolverTests
    {
        [TestMethod]
        public void IntegerAndDecimal_WillPromoteToDecimal()
        {
            Assert.AreEqual(QLType.Decimal, OperatorResultTypeResolver.ResolveOperationType(QLType.Integer, QLType.Decimal));
        }
    }
}
