using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Api.Entities;
using QL.Api.Infrastructure;
using QL.Api.Types;
using QL.Core.Infrastructure;

namespace QL.Core.Test.Validation
{
    [TestClass]
    public class DuplicateSymbolDetectorTests
    {
        [TestMethod]
        public void TwoSymbolsNoDuplicates_WillReturnEmptyList()
        {
            // Arrange
            var symbolTable = new SymbolTable();
            symbolTable.Add(new Symbol("a", QLType.Decimal, null));
            symbolTable.Add(new Symbol("b", QLType.Date, null));

            // Act & Assert
            var duplicateDetector = new DuplicateSymbolDetectionPipelineElement();
            var task = new ParsingTask("");
            task.SymbolTable = symbolTable;
            Assert.AreEqual(0, duplicateDetector.Process(task).Errors.Count);
        }

        [TestMethod]
        public void TwoSymbolsOneDuplicate_WillReturnOneEntry()
        {
            // Arrange
            var symbolTable = new SymbolTable();
            symbolTable.Add(new Symbol("a", QLType.Decimal, null));
            symbolTable.Add(new Symbol("a", QLType.Decimal, null));

            // Act & Assert
            var duplicateDetector = new DuplicateSymbolDetectionPipelineElement();
            var task = new ParsingTask("");
            task.SymbolTable = symbolTable;
            Assert.AreEqual(1, duplicateDetector.Process(task).Errors.Count);
        }

        [TestMethod]
        public void FourSymbolsOneDuplicatedTwice_WillReturnTwoEntries()
        {
            // Arrange
            var symbolTable = new SymbolTable();
            symbolTable.Add(new Symbol("a", QLType.Decimal, null));
            symbolTable.Add(new Symbol("a", QLType.Decimal, null));
            symbolTable.Add(new Symbol("a", QLType.Decimal, null));
            symbolTable.Add(new Symbol("d", QLType.Boolean, null));

            // Act & Assert
            var duplicateDetector = new DuplicateSymbolDetectionPipelineElement();
            var task = new ParsingTask("");
            task.SymbolTable = symbolTable;
            Assert.AreEqual(2, duplicateDetector.Process(task).Errors.Count);
        }

        [TestMethod]
        public void FourSymbolsTwoDuplicatedOnce_WillReturnTwoEntries()
        {
            // Arrange
            var symbolTable = new SymbolTable();
            symbolTable.Add(new Symbol("a", QLType.Decimal, null));
            symbolTable.Add(new Symbol("a", QLType.Decimal, null));
            symbolTable.Add(new Symbol("d", QLType.Decimal, null));
            symbolTable.Add(new Symbol("d", QLType.Boolean, null));

            // Act & Assert
            var duplicateDetector = new DuplicateSymbolDetectionPipelineElement();
            var task = new ParsingTask("");
            task.SymbolTable = symbolTable;
            Assert.AreEqual(2, duplicateDetector.Process(task).Errors.Count);
        }
    }
}
