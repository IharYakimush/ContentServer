using System.Text.RegularExpressions;

namespace ContentServer.Core.Conversion
{
    public class DefaultConversionAction : ConversionAction
    {
        private static readonly ParamDefinition formatParam = new ParamDefinition("f", new Regex("^(jpg|png)$"));

        public override string Name => "default";

        public override IReadOnlyCollection<ParamDefinition> SupportedParams { get; } = new List<ParamDefinition>()
        {
            new ParamDefinition("w",new Regex("^[1-9]{1}[0-9]{0,4}$",RegexOptions.Compiled & RegexOptions.Singleline)),
            formatParam
        };

        public override IReadOnlySet<string> InputFormats { get; } = new HashSet<string>(new[] { "jpg" });

        public override string OutputFormat(string inputFormat, IReadOnlyDictionary<ParamDefinition, string> actualParams)
        {
            return actualParams?[formatParam] ?? inputFormat;
        }
    }
}
