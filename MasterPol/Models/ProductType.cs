using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterPol.Models
{
    [Table("product_types")]
    public class ProductType
    {
        [Key]
        public int Id { get; set; }

        [Column("type_name"), Required, MaxLength(100)]
        public string TypeName { get; set; }

        [Column("coefficient")]
        public decimal Coefficient { get; set; }
    }
}