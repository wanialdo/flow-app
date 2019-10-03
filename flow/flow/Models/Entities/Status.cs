using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flow.Models.Entities
{
    /// <summary>
    /// The purpose of this class is to represent each status a item can stay on the flow.
    /// </summary>
    [Table("tb_status")]
    public class Status
    {
        [Key]
        [Display(Name = "#")]
        [Column("isn_status")]
        public long ID { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Status Description cannot be null.")]
        [Column("dsc_status")]
        public string Description { get; set; }
    }
}