namespace AngularCircus.web.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public bool IsDone { get; set; }

        public string Species { get; set; }
        public Exhibit Exhibit { get; set; }

        public int ExhibitId {get; set;}
        public Animal()
        {

        }
    }
}