using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botw.Formats.Json
{
    class GitHub
    {
        public Asset[] assets { get; set; }

        public class Asset
        {
            public string browser_download_url { get; set; }
        }

    }
}
