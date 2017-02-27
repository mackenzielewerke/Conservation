

using System.Collections.Generic;

namespace AngularCircus.web.Models
{
    public class Circus
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public List<Act> Acts { get; set; }
        public Circus()
        {

        }
    }
}
