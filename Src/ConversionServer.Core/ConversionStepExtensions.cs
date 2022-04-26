using System.Text.RegularExpressions;

namespace ConversionServer.Core
{
    public static class ConversionStepExtensions
    {
        private static readonly Regex funcReg = new Regex(@"^([a-z]*)\((.*)\)$");

        private static readonly Regex argReg = new Regex(@"(?:[^\)\(,]+|\([^\)\(]+\))+");
        public static IEnumerable<ConversionStep> ParseSteps(string value)
        {
            string fname = ConversionAction.DefaultName;
            var fmatch = funcReg.Match(value);
            if (fmatch.Success)
            {
                fname = string.IsNullOrEmpty(fmatch.Groups[1].Value) ? fname : fmatch.Groups[1].Value;
                value = fmatch.Groups[2].Value;
            }

            var pmatch = argReg.Match(value);
            if (!pmatch.Success)
            {
                throw new ArgumentException($"Unable to parse params {value}", nameof(value));
            }

            var pv = pmatch.Groups.Values.Select(g => g.Value);

            List<string> input = new();
            List<KeyValuePair<string, string>> param = new();

            foreach (var v in pv)
            {
                if (v.StartsWith("$", StringComparison.Ordinal))
                {
                    input.Add(v.TrimStart('$'));
                }
                else if (v.IndexOf('_', StringComparison.Ordinal) > 0)
                {
                    var s = v.Split('_', StringSplitOptions.RemoveEmptyEntries);
                    param.Add(new KeyValuePair<string, string>(s[0], string.Join('_', s.Skip(1))));
                }
                else
                {
                    throw new ArgumentException($"Unable to parse param {v} in {value} for {fname} step", nameof(value));
                }
            }

            ConversionDefinition conversion = new ConversionDefinition(fname, param.ToDictionary(kv => kv.Key, kv => kv.Value));

            ConversionStep result = new ConversionStep(conversion, "result", input);

            yield return result;
        }
    }
}
