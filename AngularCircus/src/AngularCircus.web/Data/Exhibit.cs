
using System.Collections.Generic;

namespace AngularCircus.web.Models
{
    public class Exhibit
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }

        public Zoo Zoo { get; set; }

        public virtual List<Animal> Animals { get; set; }
        public Exhibit()
        {
            Animals = new List<Animal>();
        }

    }
}