using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterPol.Models
{
    [Table("warehouse_stocks")]
    public class WarehouseStock
    {
        [Key]
        public int Id { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("last_updated")]
        public DateTime LastUpdated { get; set; }

        public virtual Product Product { get; set; }
    }
}