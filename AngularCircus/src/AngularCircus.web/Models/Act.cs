
namespace AngularCircus.web.Models
{
    public class Act
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }

        public string Performer()
        {
            var performer = new Performer();
            return (performer.ToString());
        }

        public Act()
        {
        }
    }
}