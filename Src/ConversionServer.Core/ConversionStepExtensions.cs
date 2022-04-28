using System.Text.RegularExpressions;

namespace ConversionServer.Core
{
    public static class ConversionStepExtensions
    {
        private static readonly Regex funcReg = new Regex(@"^([a-z]*)\((.*)\)$");

        private static readonly Regex argReg = new Regex(@"(?:[^\)\(,]+|\([^\)\(]+\))+");

        public static IEnumerable<ConversionStep> ParseSteps(string value)
        {
            return ParseSteps(value, true).DistinctBy(s => s.Output);
        }

        internal static IEnumerable<ConversionStep> ParseSteps(string value, bool first)
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

            List<string> pv = new List<string>();
            do
            {
                pv.Add(pmatch.Groups.Values.Select(g => g.Value).Single());
                pmatch = pmatch.NextMatch();

            } while (pmatch.Success);

            List<string> input = new();
            List<KeyValuePair<string, string>> param = new();

            foreach (var v in pv)
            {
                if (v.StartsWith("$", StringComparison.Ordinal))
                {
                    string inp = v.TrimStart('$');
                    if (v.Contains('(', StringComparison.Ordinal))
                    {
                        // reference to result of conversion
                        foreach (var item in ParseSteps(inp, false))
                        {
                            input.Add(item.Output);
                            yield return item;
                        }
                    }
                    else
                    {
                        // reference to file
                        input.Add(inp);
                    }
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

            ConversionStep result = new ConversionStep(conversion, first ? "result" : HashHelper.HashMd5(conversion.GetHash(), input), input);

            yield return result;
        }
    }
}
