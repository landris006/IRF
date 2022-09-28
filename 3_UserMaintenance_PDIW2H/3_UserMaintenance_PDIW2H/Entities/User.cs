using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_UserMaintenance_PDIW2H.Entities
{
    public class User
    {
        public Guid ID { get; set; } = new Guid();
        public string FullName { get; set; }
    }
}
