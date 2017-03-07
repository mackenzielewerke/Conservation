namespace AngularCircus.web.Models
{
    public class Performer
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public bool IsDone { get; set; }
        

        
        public Performer()
        {

        }
    }
}