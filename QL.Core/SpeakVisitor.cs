using System.Collections.Generic;
using static QL.Core.SpeakParser;

namespace QL.Core
{
    public class SpeakVisitor : SpeakBaseVisitor<object>
    {
        public List<SpeakLine> Lines = new List<SpeakLine>();

        public override object VisitLine(LineContext context)
        {
            NameContext name = context.name();
            OpinionContext opinion = context.opinion();

            SpeakLine line = new SpeakLine() { Person = name.GetText(), Text = opinion.GetText().Trim('"') };
            Lines.Add(line);

            return line;
        }
    }
}
