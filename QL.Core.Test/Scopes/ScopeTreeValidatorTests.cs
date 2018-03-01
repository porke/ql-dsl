using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Core.Scopes;
using QL.Core.Symbols;

namespace QL.Core.Test.Scopes
{
    [TestClass]
    public class ScopeTreeValidatorTests
    {
        [TestMethod]
        public void OneVariableOneReference_WillReturnTwoEmptyLists()
        {
            var ScopeTree = new Scope(parent: null);
            ScopeTree.AddVariable(new Symbol("a", SymbolType.Integer, token: null));
            ScopeTree.AddReference(new Symbol("a", SymbolType.Integer, token: null));

            var Validator = new ScopeTreeValidator();
            Validator.CheckReferencesScope(ScopeTree);

            Assert.AreEqual(0, Validator.UndeclaredVariables.Count);
            Assert.AreEqual(0, Validator.VariablesDeclaredOutOfScope.Count);
        }

        [TestMethod]
        public void OneReferencesNoVariable_WillReturnOneUnreferencedVariable()
        {
            var ScopeTree = new Scope(parent: null);
            ScopeTree.AddReference(new Symbol("a", SymbolType.Undefined, token: null));

            var Validator = new ScopeTreeValidator();
            Validator.CheckReferencesScope(ScopeTree);

            Assert.AreEqual(1, Validator.UndeclaredVariables.Count);
            Assert.AreEqual(0, Validator.VariablesDeclaredOutOfScope.Count);
        }

        [TestMethod]
        public void OneReferencesNoVariableInScope_WillReturnReferencedOutOfScope()
        {
            var ScopeTree = new Scope(parent: null);
            ScopeTree.AddReference(new Symbol("a", SymbolType.Integer, token: null));
            var SecondScope = new Scope(ScopeTree);
            SecondScope.AddVariable(new Symbol("a", SymbolType.Integer, token: null));
            ScopeTree.Childeren.Add(SecondScope);

            var Validator = new ScopeTreeValidator();
            Validator.CheckReferencesScope(ScopeTree);

            Assert.AreEqual(0, Validator.UndeclaredVariables.Count);
            Assert.AreEqual(1, Validator.VariablesDeclaredOutOfScope.Count);
        }
    }
}
