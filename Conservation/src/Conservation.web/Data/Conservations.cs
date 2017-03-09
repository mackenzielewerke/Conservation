using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conservation.web.Data
{
    public class Conservations
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public List<Habitat> Habitats { get; set; }
        public Conservations()
        {
            Habitats = new List<Habitat>();
        }

    }
}
