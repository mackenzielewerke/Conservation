using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conservation.web.Data
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public Habitat Habitat { get; set; }
        public int HabitatId { get; set; }
        public Animal()
        {

        }
    }
}
