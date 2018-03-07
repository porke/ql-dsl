using System.Linq;
using System.Collections.Generic;
using QL.Core.Types;
using System;

namespace QL.Core.Operators
{
    internal static class OperatorResultTypeResolver
    {
        private class TypePair : IEquatable<TypePair>
        {
            internal TypePair(QLType first, QLType second)
            {
                FirstType = first;
                SecondType = second;
            }

            internal TypePair GetSwappedPair()
            {
                return new TypePair(SecondType, FirstType);
            }

            public override int GetHashCode()
            {
                return $"{FirstType} {SecondType}".GetHashCode();
            }

            public bool Equals(TypePair other)
            {
                return FirstType == other.FirstType && SecondType == other.SecondType;
            }

            public QLType FirstType { get; }
            public QLType SecondType { get; }
        }

        private static Dictionary<TypePair, QLType> _typeResolutions = new Dictionary<TypePair, QLType>();

        static OperatorResultTypeResolver()
        {
            _typeResolutions.Add(new TypePair(QLType.Integer, QLType.Decimal), QLType.Decimal);

            var invertedResolutions = _typeResolutions.Select(kvp => new KeyValuePair<TypePair, QLType>(kvp.Key.GetSwappedPair(), kvp.Value))
                                                      .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            foreach (var resolution in invertedResolutions)
            {
                _typeResolutions.Add(resolution.Key, resolution.Value);
            }
        }

        public static QLType ResolveOperationType(QLType lhs, QLType rhs)
        {
            if (lhs == rhs)
            {
                return lhs;
            }

            return _typeResolutions[new TypePair(lhs, rhs)];
        }
    }
}
