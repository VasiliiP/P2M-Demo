using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Play2Money.Models
{
    public class PlayerViewModel
    {
        public PlayerViewModel(Player player)
        {
            PlayerId = player.Id;
            Points = player.Points;
            Orders = player.Orders.Select(order => new OrderViewModel(order)).ToList();
            GlobalUid = player.GlobalUid;
        }

        public PlayerViewModel()
        {
            
        }
        public int PlayerId { get; set; }
        public string GlobalUid { get; set; }
        public int Points { get; set; } 
        public List<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();
    }
}