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

        private IReadOnlyList<string> HarvestParsingErrors(SymbolTable symbolTable, Node ast)
        {
            var duplicateSymbolDetector = new DuplicateSymbolDetector();
            var errors = new List<Error>();

            errors.AddRange(duplicateSymbolDetector.FindDuplicateSymbols(symbolTable));

            var scopeTreeVisitor = new ScopeExtractingVisitor(symbolTable);
            ast.Accept(scopeTreeVisitor);
            var Validator = new ScopeTreeValidator();
            errors.AddRange(Validator.CheckReferencesScope(scopeTreeVisitor.ScopeTree));

            var textErrors = new List<string>();
            foreach (Error error in errors)
            {
                textErrors.Add(error.ToString());
            }
            return textErrors;
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
                var errors = HarvestParsingErrors(symbols, ast);

                return new ParsedSymbols(ast, symbols, errors);
            }
            catch (ParsingFailureException ex)
            {
                return new ParsedSymbols(new NullNode(), new SymbolTable(), ex.ParsingErrors);
            }
        }
    }
}
