using System.Collections.Generic;
using System.Linq;

namespace Play2Money.Models
{
    public class OrderListViewModel
    {
        public OrderListViewModel()
        {
            Orders = new List<OrderTableViewModel>();
        }
        public List<OrderTableViewModel> Orders { get; set;}

        public int UncompletedCount => Orders.ToList().Count;
    }
}