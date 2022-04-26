using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConversionServer.Core.Tests.Internal;

public class DefaultTestConversionAction : ConversionAction
{
    public override string Name => DefaultName;
    public override IReadOnlySet<string> InputFormats { get; } = new HashSet<string>(new[] { "jpg", "png" });

    public override IReadOnlyDictionary<string, Func<string, string?>> SupportedParams { get; } = new Dictionary<string, Func<string, string?>>()
    {
        { "f", (v) => ValidateRegexp(v, new Regex("^(jpg|png)$")) },
        { "w", (v) => ValidateRegexp(v, new Regex("^[1-9]{1}[0-9]{0,4}$")) },
    };

    public override FileDefinition OutputFormat(IReadOnlyCollection<FileDefinition> inputFormats, IReadOnlyDictionary<string, string> actualParams)
    {
        if (actualParams == null) throw new ArgumentNullException(nameof(actualParams));

        FileDefinition result = inputFormats.Single();
        Dictionary<string, string> hashParams = new Dictionary<string, string>(actualParams);

        if (actualParams.ContainsKey("f"))
        {
            if (actualParams["f"] != result.Format)
            {
                result = result with { Format = actualParams["f"] };
            }
        }
        else
        {
            hashParams.Add("f", result.Format);
        }

        result = result with { Etag = HashHelper.HashMd5(ConversionDefinition.GetHash(this.Name, hashParams), inputFormats.Select(i => i.Etag)) };

        return result;
    }
}