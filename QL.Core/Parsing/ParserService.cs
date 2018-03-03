using QL.Core.Api;
using Antlr4.Runtime;
using System.Collections.Generic;
using QL.Core.Ast;
using QL.Core.Symbols;
using System.Linq;
using QL.Core.Scopes;
using QL.Core.Errors;

namespace QL.Core.Parsing
{
    internal class ParserService : IParserService
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

        private IReadOnlyList<string> HarvestParsingErrors(SymbolTable symbolTable, Node ast, Scope scopeTree)
        {
            var errors = new List<Error>();

            var duplicateSymbolDetector = new DuplicateSymbolDetector();
            errors.AddRange(duplicateSymbolDetector.FindDuplicateSymbols(symbolTable));

            var scopeTreeValidator = new ScopeTreeValidator();
            errors.AddRange(scopeTreeValidator.CheckReferencesScope(scopeTree));
           
            return errors.Select(err => err.ToString()).ToList();
        }

        private Scope DeriveScopeTree(SymbolTable symbolTable, Node ast)
        {
            var scopeExtractingVisitor = new ScopeExtractingVisitor(symbolTable);
            ast.Accept(scopeExtractingVisitor);
            return scopeExtractingVisitor.ScopeTree;
        }

        public ParsedSymbols ParseQLInput(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return new ParsedSymbols(new NullNode(),
                                         new SymbolTable(),
                                         new Scope(null),
                                         new List<string>());
            }

            QLParser parser = SetupParser(input);
            try
            {
                Node ast = ExtractAst(parser);
                SymbolTable symbols = ExtractSymbols(ast);
                Scope scopeTree = DeriveScopeTree(symbols, ast);
                var errors = HarvestParsingErrors(symbols, ast, scopeTree);

                return new ParsedSymbols(ast, symbols, scopeTree, errors);
            }
            catch (ParsingFailureException ex)
            {
                return new ParsedSymbols(new NullNode(), new SymbolTable(), new Scope(null), ex.ParsingErrors);
            }
        }
    }
}
