using Antlr4.Runtime;
using QL.Api.Entities;
using System;

namespace QL.Core.Parsing
{
    internal static class QLTypeConverter
    {
        public static QLType FromTokenTypeToQLType(IToken token)
        {
            QLType type = QLType.Undefined;
            switch (token.Type)
            {
                case QLParser.INTEGER:
                    type = QLType.Integer;
                    break;
                case QLParser.BOOLEAN:
                    type = QLType.Boolean;
                    break;
                case QLParser.DECIMAL:
                    type = QLType.Decimal;
                    break;
                case QLParser.DATE:
                    type = QLType.Date;
                    break;
                case QLParser.STRING:
                    type = QLType.String;
                    break;
            }
            return type;
        }

        public static QLType FromStringTypeToQLType(string text)
        {
            QLType type;
            try
            {
                type = (QLType)Enum.Parse(typeof(QLType), text, ignoreCase: true);
            }
            catch (ArgumentException)
            {
                type = QLType.Undefined;
            }
            return type;
        }
    }
}
