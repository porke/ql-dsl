using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Core.Errors;
using QL.Core.Scopes;
using QL.Core.Symbols;
using QL.Core.Types;
using System.Collections.Generic;

namespace QL.Core.Test.Scopes
{
    [TestClass]
    public class ScopeTreeValidatorTests
    {
        [TestMethod]
        public void OneVariableOneReference_WillReturnTwoEmptyLists()
        {
            var ScopeTree = new Scope(parent: null);
            ScopeTree.AddVariable(new Symbol("a", QLType.Integer, token: null));
            ScopeTree.AddReference(new Symbol("a", QLType.Integer, token: null));

            var Validator = new ScopeTreeValidator();
            List<Error> Errors = Validator.CheckReferencesScope(ScopeTree);

            Assert.AreEqual(0, Errors.Count);
        }

        [TestMethod]
        public void OneReferencesNoVariable_WillReturnOneUnreferencedVariable()
        {
            var ScopeTree = new Scope(parent: null);
            ScopeTree.AddReference(new Symbol("a", QLType.Undefined, token: null));

            var Validator = new ScopeTreeValidator();
            List<Error> Errors = Validator.CheckReferencesScope(ScopeTree);

            Assert.AreEqual(1, Errors.Count);
        }

        [TestMethod]
        public void OneReferencesNoVariableInScope_WillReturnReferencedOutOfScope()
        {
            var ScopeTree = new Scope(parent: null);
            ScopeTree.AddReference(new Symbol("a", QLType.Integer, token: null));
            var SecondScope = new Scope(ScopeTree);
            SecondScope.AddVariable(new Symbol("a", QLType.Integer, token: null));
            ScopeTree.Childeren.Add(SecondScope);

            var Validator = new ScopeTreeValidator();
            List<Error> Errors = Validator.CheckReferencesScope(ScopeTree);

            Assert.AreEqual(1, Errors.Count);
        }
    }
}
