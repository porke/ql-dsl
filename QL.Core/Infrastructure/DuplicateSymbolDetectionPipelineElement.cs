using System;
using QL.Api.Infrastructure;
using System.Collections.Generic;
using QL.Core.Errors;

namespace QL.Core.Infrastructure
{
    internal class DuplicateSymbolDetectionPipelineElement : IPipelineElement<ParsingTask>
    {
        public bool CanContinue => true;

        public ParsingTask Process(ParsingTask input)
        {
            var errors = new List<Error>();
            var symbolNames = new HashSet<string>();
            foreach (var symbol in input.SymbolTable)
            {
                if (symbolNames.Contains(symbol.Name))
                {
                    errors.Add(new VariableDuplicate(symbol.Name, symbol.Token?.Line ?? 0));
                }

                symbolNames.Add(symbol.Name);
            }

            errors.ForEach(x => input.Errors.Add(x.ToString()));
            return input;
        }
    }
}
