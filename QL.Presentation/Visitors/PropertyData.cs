namespace Presentation.Visitors
{
    internal class PropertyData
    {
        public PropertyData(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public string Value { get; }
    }
}