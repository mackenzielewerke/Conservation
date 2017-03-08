

using System.Collections.Generic;

namespace AngularCircus.web.Models
{
    public class Zoo
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public List<Exhibit> Exhibits { get; set; }
        public Zoo()
        {
            Exhibits = new List<Exhibit>();
        }
    }
}
