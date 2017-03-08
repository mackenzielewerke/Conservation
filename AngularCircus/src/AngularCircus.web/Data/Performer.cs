namespace AngularCircus.web.Models
{
    public class Performer
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public bool IsDone { get; set; }
        public Act Act { get; set; }

        public int ActId {get; set;}
        public Performer()
        {

        }
    }
}