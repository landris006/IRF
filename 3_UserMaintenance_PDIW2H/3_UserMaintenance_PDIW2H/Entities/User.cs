using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_UserMaintenance_PDIW2H.Entities
{
    internal class User
    {
        public Guid ID { get; set; } = new Guid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return string.Format(
                    "{0} {1}",
                    FirstName,
                    LastName);
            }
        }
    }
}
