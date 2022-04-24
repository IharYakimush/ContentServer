using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContentServer.Core.Conversion
{
    public static class ConversionStepExtensions
    {
        private static readonly Regex funcReg = new Regex(@"^([a-z]*)\((.*)\)$");

        private static readonly Regex argReg = new Regex(@"(?:[^\)\(,]+|\([^\)\(]+\))+");
        public static IReadOnlyCollection<ConversionStep> Parse(string value)
        {

            return new ConversionStep[0];
        }

        private static ConversionStep ParseStep(string value)
        {
            string fname = "default";
            var fmatch = funcReg.Match(value);
            if (fmatch.Success)
            {
                fname = fmatch.Groups[0].Value ?? fname;
                value = fmatch.Groups[1].Value;
            }

            var pmatch = argReg.Match(value);
            if (!pmatch.Success)
            {
                throw new ArgumentException();
            }

            throw new NotImplementedException();
        }
    }
}
