using ImageMagick;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContentServer.Core.Conversion
{
    public abstract class ConversionAction
    {
        public abstract string Name { get; }
        public abstract IReadOnlyCollection<ParamDefinition> SupportedParams { get; }
        public abstract IReadOnlySet<string> InputFormats { get; }
        public abstract string OutputFormat(string inputFormat, IReadOnlyDictionary<ParamDefinition, string> actualParams);
    }

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
