namespace TeamAuthentication.Models
{
    public class Circus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Performer Performers { get; set; }
        public Animal Animals { get; set; }
        public Ticket Tickets { get; set; }
        public Circus()
        {

        }
    }
}