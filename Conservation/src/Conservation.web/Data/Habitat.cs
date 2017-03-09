using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conservation.web.Data
{
    public class Habitat
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public Conservations Conservation { get; set; }
        public int ConservationId { get; set; }
        public List<Animal> Animals { get; set; }
        public Habitat()
        {
            Animals = new List<Animal>();
        }
    }
}
