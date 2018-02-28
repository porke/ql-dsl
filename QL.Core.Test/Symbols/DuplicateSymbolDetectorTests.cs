using Antlr4.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Core.Symbols;
using System.Collections.Generic;

namespace QL.Core.Test.Symbols
{
    [TestClass]
    public class DuplicateSymbolDetectorTests
    {
        [TestMethod]
        public void TwoSymbolsNoDuplicates_WillReturnEmptyList()
        {
            // Arrange
            var symbolTable = new SymbolTable();
            symbolTable.Add(new Symbol("a", SymbolType.Decimal, null));
            symbolTable.Add(new Symbol("b", SymbolType.Date, null));

            // Act & Assert
            var duplicateDetector = new DuplicateSymbolDetector();
            Assert.AreEqual(0, duplicateDetector.FindDuplicateSymbols(symbolTable).Count);
        }

        [TestMethod]
        public void TwoSymbolsOneDuplicate_WillReturnOneEntry()
        {
            // Arrange
            var symbolTable = new SymbolTable();
            symbolTable.Add(new Symbol("a", SymbolType.Decimal, null));
            symbolTable.Add(new Symbol("a", SymbolType.Decimal, null));

            // Act
            var duplicateDetector = new DuplicateSymbolDetector();
            IReadOnlyList<Symbol> duplicateSymbols = duplicateDetector.FindDuplicateSymbols(symbolTable);

            // Assert
            Assert.AreEqual(1, duplicateSymbols.Count);
            Assert.AreEqual("a", duplicateSymbols[0].Name);
        }

        [TestMethod]
        public void FourSymbolsOneDuplicatedTwice_WillReturnTwoEntries()
        {
            // Arrange
            var symbolTable = new SymbolTable();
            symbolTable.Add(new Symbol("a", SymbolType.Decimal, null));
            symbolTable.Add(new Symbol("a", SymbolType.Decimal, null));
            symbolTable.Add(new Symbol("a", SymbolType.Decimal, null));
            symbolTable.Add(new Symbol("d", SymbolType.Boolean, null));

            // Act
            var duplicateDetector = new DuplicateSymbolDetector();
            IReadOnlyList<Symbol> duplicateSymbols = duplicateDetector.FindDuplicateSymbols(symbolTable);

            // Assert
            Assert.AreEqual(2, duplicateSymbols.Count);
            Assert.AreEqual("a", duplicateSymbols[0].Name);
            Assert.AreEqual("a", duplicateSymbols[1].Name);
        }

        [TestMethod]
        public void FourSymbolsTwoDuplicatedOnce_WillReturnTwoEntries()
        {
            // Arrange
            var symbolTable = new SymbolTable();
            symbolTable.Add(new Symbol("a", SymbolType.Decimal, null));
            symbolTable.Add(new Symbol("a", SymbolType.Decimal, null));
            symbolTable.Add(new Symbol("d", SymbolType.Decimal, null));
            symbolTable.Add(new Symbol("d", SymbolType.Boolean, null));

            // Act
            var duplicateDetector = new DuplicateSymbolDetector();
            IReadOnlyList<Symbol> duplicateSymbols = duplicateDetector.FindDuplicateSymbols(symbolTable);

            // Assert
            Assert.AreEqual(2, duplicateSymbols.Count);
            Assert.AreEqual("a", duplicateSymbols[0].Name);
            Assert.AreEqual("d", duplicateSymbols[1].Name);
        }
    }
}
