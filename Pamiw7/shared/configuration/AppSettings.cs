using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pamiw6.Shared.Configuration
{
    public class AppSettings
    {
        public string BaseAPIUrl { get; set; }
        public BookEndpoint BookEndpoint { get; set; }
        public AuthorEndpoint AuthorEndpoint { get; set; }
    }
}
