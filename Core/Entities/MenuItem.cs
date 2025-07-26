using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class MenuItem
    {
        public string Label { get; set; }
        public string Route { get; set; }
        public string Icon { get; set; }
        public int Order { get; set; } 
    }
}
