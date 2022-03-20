using System.Text.RegularExpressions;

namespace ContentServer.Core.Conversion
{
    public abstract class ConversionAction
    {
        public abstract string Name { get; }
        public abstract IReadOnlyDictionary<string,Func<string,string?>> SupportedParams { get; }
        public abstract IReadOnlyCollection<string> InputFormats { get; }
        public virtual int MinInputCount { get; } = 1;
        public virtual int MaxInputCount { get; } = 1;
        public abstract FileDefinition OutputFormat(IReadOnlyCollection<FileDefinition> inputFormats, IReadOnlyDictionary<string, string> actualParams);

        protected static string? ValidateRegexp(string value, Regex pattern)
        {
            if (pattern.IsMatch(value))
            {
                return null;
            }

            return $"Value {value} not match {pattern} pattern.";
        }
    }
}
