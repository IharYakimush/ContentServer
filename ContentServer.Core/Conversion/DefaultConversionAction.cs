using System.Text.RegularExpressions;

namespace ContentServer.Core.Conversion
{
    public class DefaultConversionAction : ConversionAction
    {
        public override string Name => "default";

        public override IReadOnlySet<string> InputFormats { get; } = new HashSet<string>(new[] { "jpg", "png" });

        public override IReadOnlyDictionary<string, Func<string, string?>> SupportedParams { get; } = new Dictionary<string, Func<string, string?>>() 
        { 
            { "f", (v) => ValidateRegexp(v, new Regex("^(jpg|png)$")) },
            { "w", (v) => ValidateRegexp(v, new Regex("^[1-9]{1}[0-9]{0,4}$")) },
        };

        public override string OutputFormat(IReadOnlyCollection<string> inputFormats, IReadOnlyDictionary<string, string> actualParams)
        {
            return actualParams?["f"] ?? inputFormats.First();
        }
    }
}
