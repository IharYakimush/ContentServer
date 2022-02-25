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
        internal Regex UrlRegex = new Regex("^/([^/]*)/(.*).([a-z,A-Z,0-1]*)$", RegexOptions.Compiled & RegexOptions.Singleline);
        public string UrlPattern 
        {
            get => this.UrlRegex.ToString();
            set => this.UrlRegex = new Regex(value);
        }

        public string UrlTenant { get; set; } = "$1";

        public string UrlMain { get; set; } = "$2";

        public string UrlFormat { get; set; } = "$3";
    }
}
