using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RoastTrackr.Models
{
    [Table("Coffees")]
    public class Coffee
    {
        public Coffee()
        {
            this.Batches = new HashSet<Batch>();
        }

        [Key]
        public int CoffeeId { get; set; }
        public string CoffeeName { get; set; }
        public int Inventory { get; set; }
        public virtual ICollection<Batch> Batches { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
