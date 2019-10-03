using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flow.Models.Entities
{
    /// <summary>
    /// The purpose os this classe is to represent the flow itself and the bindings between each
    /// step on the diagram.
    /// </summary>
    [Table("tb_flow")]
    public class MainFlow
    {
        [Key]
        [Display(Name = "#")]
        [Column("isn_flow")]
        public long ID { get; set; }

        [Display(Name = "Init Status")]
        [Column("isn_status_start")]
        [Required(ErrorMessage = "Init Status cannot be null.")]
        public long FlowInitStatusID { get; set; }

        [Display(Name = "Destination Status")]
        [Column("isn_status_end")]
        [Required(ErrorMessage = "Destination Status cannot be null.")]
        public long FlowEndStatusID { get; set; }

        /// <summary>
        /// Bind here with your structure responsible for this status position.
        /// </summary>
        [Display(Name = "Responsible")]
        [Column("dsc_responsible")]
        [Required(ErrorMessage = "The Responsible for this status position cannot be null.")]
        public string Responsible { get; set; }

        /// <summary>
        /// That's the name that will be shown on button
        /// </summary>
        [Display(Name = "Action")]
        [Column("dsc_action")]
        [Required(ErrorMessage = "Action name cannot be null")]
        public string ActionName { get; set; }

        /// <summary>
        /// If it's informed, implement your rules to guarantee that periods are
        /// evaluated on steps of the flow.
        /// </summary>
        [Display(Name = "Analisys Period in Days")]
        [Column("num_analisys_period")]
        public int? MaxAnalisysPeriod { get; set; }

        /// <summary>
        /// If you like to, inform the number of the step in the diagram that represents this flow.
        /// This will be used to draw a graphic view of the flow.
        /// </summary>
        [Display(Name = "Diagram Step Number")]
        [Column("num_step")]
        [Required(ErrorMessage = "Diagram step cannot be null.")]
        public int StepNumber { get; set; }

        [Display(Name = "Init Status")]
        [ForeignKey("FlowInitStatusID")]
        public Status FlowInitStatus { get; set; }

        [Display(Name = "Destination Status")]
        [ForeignKey("FlowEndStatusID")]
        public Status FlowEndStatus { get; set; }
    }
}