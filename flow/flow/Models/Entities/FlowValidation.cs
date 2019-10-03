using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flow.Models.Entities
{
    /// <summary>
    /// In this class we represent a validation about one item. We will use the flow to determine where
    /// is the item now and where it can be on the next validation. Movements are made on business.
    /// </summary>
    [Table("tb_historico", Schema = "capacitacao")]
    public class FlowValidation
    {
        [Display(Name = "ID")]
        [Key]
        [Column("isn_validation")]
        public long ID { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Status cannot be empty.")]
        [Column("isn_status")]
        public long StatusID { get; set; }

        /// <summary>
        /// Bind with Users repository
        /// </summary>
        [Display(Name = "User")]
        [Required(ErrorMessage = "User cannot be empty")]
        [Column("isn_usuario")]
        public long UserCode { get; set; }

        /// <summary>
        /// Item been availed in the flow.
        /// </summary>
        [Display(Name = "Document")]
        [Required(ErrorMessage = "Document cannot be empty")]
        [Column("isn_document")]
        public long DocumentCode { get; set; }

        [Display(Name = "Track Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date")]
        [Required(ErrorMessage = "Date cannot be empty")]
        [Column("dth_analisys")]
        public DateTime AnalisysDate { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Analisys description cannot be empty")]
        [Column("dsc_analisys")]
        public string Description { get; set; }

        [Display(Name = "Complementary info")]
        [Column("dsc_complement")]
        public string ComplementaryInfo { get; set; }

        [Display(Name = "Status")]
        public virtual Status Status { get; set; }
    }
}