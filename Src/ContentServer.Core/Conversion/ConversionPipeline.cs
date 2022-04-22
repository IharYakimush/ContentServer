
using ContentServer.Core.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ContentServer.Core.Conversion
{
    public class ConversionPipeline
    {
        public ConversionPipeline(IReadOnlyCollection<ConversionStep> steps, IReadOnlyDictionary<string, FileDefinition> inputs)
        {
            this.Steps = steps ?? throw new ArgumentNullException(nameof(steps));
            Inputs = inputs ?? throw new ArgumentNullException(nameof(inputs));
        }

        public IReadOnlyCollection<ConversionStep> Steps { get; }
        public IReadOnlyDictionary<string, FileDefinition> Inputs { get; }

        public virtual bool Validate(
            IReadOnlyDictionary<string, ConversionAction> actions,
            out FileDefinition? output,
            out string? description) 
        {
            output = null;
            foreach (var item in this.Inputs)
            {
                if (string.IsNullOrWhiteSpace(item.Value.Format))
                {
                    description = $"Input file with alias {item.Key}. Format not provided";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(item.Value.Etag))
                {
                    description = $"Input file with alias {item.Key}. ETag not provided";
                    return false;
                }
            }

            if (!this.ValidateFormat(actions, out output, out description))
            {
                return false;
            }            

            return true;
        }
    }
}
