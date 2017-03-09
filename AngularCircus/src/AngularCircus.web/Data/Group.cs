
using System.Collections.Generic;

namespace AngularZoo.web.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }

        public Conservation Conservation { get; set; }

        public virtual List<Animal> Animals { get; set; }
        public Group()
        {
            Animals = new List<Animal>();
        }

    }
}