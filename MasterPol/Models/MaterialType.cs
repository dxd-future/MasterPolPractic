using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterPol.Models
{
    [Table("material_types")]
    public class MaterialType
    {
        [Key]
        public int Id { get; set; }

        [Column("material_name"), Required, MaxLength(100)]
        public string MaterialName { get; set; }

        [Column("defect_rate")]
        public decimal DefectRate { get; set; }
    }
}