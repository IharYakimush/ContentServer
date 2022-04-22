﻿using ContentServer.Core.Helpers;

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

        public override FileDefinition OutputFormat(IReadOnlyCollection<FileDefinition> inputFormats, IReadOnlyDictionary<string, string> actualParams)
        {
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
}