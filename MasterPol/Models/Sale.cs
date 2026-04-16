using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterPol.Models
{
    [Table("sales")]
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("partner_id")]
        public int PartnerId { get; set; }

        public int Quantity { get; set; }

        [Column("sale_date")]
        public DateTime SaleDate { get; set; }

        public virtual Product Product { get; set; }
        public virtual Partner Partner { get; set; }
    }
}