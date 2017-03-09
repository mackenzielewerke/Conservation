using System;

namespace AngularZoo.web.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string PriceType { get; set; }
        public bool IsDone { get; set; }
        public Conservation Conservation { get; set; }
        public DateTime ShowDate { get; set; }

        public Ticket()
        {

        }

        public Ticket(decimal price, string pricetype, string conservation, DateTime showdate)
        {
            Conservation.Name = conservation;
            PriceType = pricetype;
            ShowDate = showdate;
            Price = price;

        }

    }
}