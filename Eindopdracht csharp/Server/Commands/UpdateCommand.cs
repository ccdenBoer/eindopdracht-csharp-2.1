using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands
{
    public class UpdateCommand
    {
        public string id { get; set; }
        public string[] messages { get; set; }
    }
}
