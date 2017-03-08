
using System.Collections.Generic;

namespace AngularCircus.web.Models
{
    public class Act
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }

        public Circus Circus { get; set; }

        public virtual List<Performer> Performers { get; set; }
        public Act()
        {
            Performers = new List<Performer>();
        }

    }
}