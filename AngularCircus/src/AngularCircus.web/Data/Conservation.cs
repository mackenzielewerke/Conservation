

using System.Collections.Generic;

namespace AngularZoo.web.Models
{
    public class Conservation
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public List<Group> Groups { get; set; }
        public Conservation()
        {
            Groups = new List<Group>();
        }
    }
}
