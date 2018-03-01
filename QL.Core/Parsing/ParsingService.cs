using QL.Core.Api;
using Antlr4.Runtime;
using System.Collections.Generic;
using QL.Core.Ast;
using QL.Core.Symbols;
using System.Linq;
using QL.Core.Scopes;

namespace QL.Core.Parsing
{
    internal class ParsingService : IParsingService
    {
        private QLParser SetupParser(string text)
        {
            var inputStream = new AntlrInputStream(text);
            var speakLexer = new QLLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(speakLexer);
            return new QLParser(commonTokenStream);
        }

        private Node ExtractAst(QLParser parser)
        {
            var errorListener = new ErrorListener();
            parser.AddErrorListener(errorListener);
            var visitor = new ParseTreeVisitor();
            Node ast = visitor.Visit(parser.form());

            if (errorListener.Errors.Count > 0)
            {
                throw new ParsingFailureException(errorListener.Errors);
            }

            return ast;
        }

        private SymbolTable ExtractSymbols(Node ast)
        {
            var symbolTableVisitor = new SymbolExtractingVisitor();
            ast.Accept(symbolTableVisitor);
            return symbolTableVisitor.SymbolTable;
        }

        private IReadOnlyList<string> HarvestDuplicateSymbolErrors(SymbolTable symbolTable)
        {
            var duplicateSymbolDetector = new DuplicateSymbolDetector();
            IReadOnlyList<Symbol> duplicateSymbols = duplicateSymbolDetector.FindDuplicateSymbols(symbolTable);
            return duplicateSymbols.Select(x => $"The variable \"{x.Name}\" in line {x.Token.Line} is already declared.").ToList();
        }

        private IReadOnlyList<string> HarvestDeclarationErrors(SymbolTable symbols, Node ast)
        {
            var scopeTreeVisitor = new ScopeExtractingVisitor(symbols);
            ast.Accept(scopeTreeVisitor);
            var Validator = new ScopeTreeValidator();
            Validator.CheckReferencesScope(scopeTreeVisitor.ScopeTree);

            List<Symbol> undecalredVariables = Validator.UndeclaredVariables;
            IReadOnlyList<string> undecalredVariableErrors =
                undecalredVariables.Select(x => $"The variable \"{x.Name}\" in line {x.Token.Line} in undeclared.").ToList();

            List<Symbol> variablesDeclaredOutOfScope = Validator.VariablesDeclaredOutOfScope;
            IReadOnlyList<string> variableDeclaredOutOfScopeErrors =
                variablesDeclaredOutOfScope.Select(x => $"The variable \"{x.Name}\" in line {x.Token.Line} is declared in a different scope.").ToList();

            return undecalredVariableErrors.Concat
                     (variableDeclaredOutOfScopeErrors).ToList();
        }

        public ParsedSymbols ParseQLInput(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return new ParsedSymbols(new NullNode(),
                                         new SymbolTable(),
                                         new List<string>());
            }

            QLParser parser = SetupParser(input);

            try
            {
                Node ast = ExtractAst(parser);
                SymbolTable symbols = ExtractSymbols(ast);
                var errors = HarvestDuplicateSymbolErrors(symbols).Concat
                     (HarvestDeclarationErrors(symbols, ast)).ToList();

                return new ParsedSymbols(ast, symbols, errors);
            }
            catch (ParsingFailureException ex)
            {
                return new ParsedSymbols(new NullNode(), new SymbolTable(), ex.ParsingErrors);
            }
        }
    }
}
