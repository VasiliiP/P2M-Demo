using System.Collections.Generic;

namespace Play2Money.Models {
    public class Player {
        public Player (RequestViewModel requestVM) {
            GlobalUid = requestVM.Id;
            Points = requestVM.P;
            var orderCount = Points/500;
            for (int i = 0; i < orderCount; i++) {
                Orders.Add (new Order ());
            }
        }

        public Player () {}
        public int Id { get; set; }

        /// <summary>
        /// Id in Game App
        /// </summary>
        public string GlobalUid { get; set; }

        public int Points { get; set; }

        public List<Order> Orders { get; set; } = new List<Order> ();

    }
}