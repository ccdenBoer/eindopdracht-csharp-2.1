using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands
{
    public struct Command
    {
        public string id { get; set; }
        public dynamic data { get; set; }
    }
}
