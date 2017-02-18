
namespace AngularCircus.web.Models
{
    public class Act
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public Performer Performer { get; set; }
        public Animal Animal { get; set; }


        public Act()
        {
        }
    }
}