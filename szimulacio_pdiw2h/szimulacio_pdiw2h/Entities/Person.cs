using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace szimulacio_pdiw2h.Entities
{
    public class Person
    {
        public uint BirthYear { get; set; }
        public Gender Gender { get; set; }
        public uint NumberOfChildren { get; set; }
        public bool IsAlive { get; set; }

        public Person()
        {
            IsAlive = true;
        }
    }
}
