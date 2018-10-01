using System;

namespace Play2Money.Models
{
    public class Order
    {
        public Order()
        {
            Created = DateTime.Now;
        }
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public string Reference { get; set; }
        public bool IsDone { get; set; }
        public DateTime Created { get; set; }

    }
}