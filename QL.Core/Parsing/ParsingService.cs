using QL.Core.Api;
using Antlr4.Runtime;
using System.Collections.Generic;
using QL.Core.Ast;
using QL.Core.Symbols;
using System;

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
                return new ParsedSymbols(ast, symbols, new List<string>());
            }
            catch (ParsingFailureException ex)
            {
                return new ParsedSymbols(new NullNode(), new SymbolTable(), ex.ParsingErrors);
            }
        }
    }
}
