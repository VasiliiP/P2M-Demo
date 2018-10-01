using System;
using System.ComponentModel.DataAnnotations;

namespace Play2Money.Models {
    public class OrderViewModel {
        
        public OrderViewModel(Order order)
        {
            Id = order.Id;
            Reference = order.Reference;
            IsDone = order.IsDone;
            Player = order.Player;
            Created = order.Created;
        }

        public OrderViewModel(){
            
        }
        public int Id { get; set; }
        
        [Url]
        public string Reference { get; set; }
        public bool IsDone { get; set; }
        public bool IsReadOnly => !string.IsNullOrEmpty(Reference);
        public Player Player { get; set; }
        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Created {get; set;}
        public string PlayerGlobalUid => Player?.GlobalUid;
    }
}