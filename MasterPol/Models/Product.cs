using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterPol.Models
{
    [Table("products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Column("product_name"), Required, MaxLength(200)]
        public string ProductName { get; set; }

        [Column("product_type_id")]
        public int ProductTypeId { get; set; }

        [Column("material_type_id")]
        public int? MaterialTypeId { get; set; }

        [MaxLength(20)]
        public string Article { get; set; }

        [Column("min_cost_for_partner")]
        public decimal? MinCostForPartner { get; set; }
    }
}