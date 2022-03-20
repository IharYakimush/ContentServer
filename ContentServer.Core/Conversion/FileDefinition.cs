using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentServer.Core.Conversion
{
    public record FileDefinition (string Id, string Etag,string Format);
}
