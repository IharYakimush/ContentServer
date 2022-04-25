using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContentServer.Core
{
    public class ContentServerOptions
    {
        internal Regex UrlRegex = new Regex("^/([^/]*)/(.*)\\.([a-z,A-Z,0-1]*)$", RegexOptions.Compiled & RegexOptions.Singleline);
        public string RegexPattern 
        {
            get => this.UrlRegex.ToString();
            set => this.UrlRegex = new Regex(value);
        }

        public string TenantReplacement { get; set; } = "$1";

        public string MainReplacement { get; set; } = "$2";

        public string FormatReplacement { get; set; } = "$3";
    }
}
