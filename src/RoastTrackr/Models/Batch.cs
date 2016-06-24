using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RoastTrackr.Models
{
    [Table("Batches")]
    public class Batch
    {
        [Key]
        public int BatchId { get; set; }
        public string CoffeeName { get; set; }
        public int BatchWeight { get; set; }
        public int BatchTime { get; set; }
        public int EndTemp { get; set; }
        public int CoffeeId { get; set; }
        public virtual Coffee Coffee { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
