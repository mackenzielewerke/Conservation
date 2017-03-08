using System;

namespace AngularCircus.web.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string PriceType { get; set; }
        public bool IsDone { get; set; }
        public Circus Circus { get; set; }
        public DateTime ShowDate { get; set; }

        public Ticket()
        {

        }

        public Ticket(decimal price, string pricetype, string circus, DateTime showdate)
        {
            Circus.Name = circus;
            PriceType = pricetype;
            ShowDate = showdate;
            Price = price;

        }

    }
}