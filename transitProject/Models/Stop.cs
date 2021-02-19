using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace transitProject.Models
{
    public class Stop
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        [Required(ErrorMessage = "Please enter the Stop ID")]
        [Range(1, 199, ErrorMessage = "Stop ID should be between 1 and 199")]
        [DisplayName("Stop ID")]
        public int stopId { get; set; }
        //
        [Required(ErrorMessage = "Please enter the Stop name")]
        [DisplayName("Stop Name")]
        public string stopName { get; set; }
        //
        [Required(ErrorMessage = "Please enter the Source")]
        [DisplayName("Source")]
        public string source { get; set; }
        //
        [Required(ErrorMessage = "Please enter the Destination")]
        
        [DisplayName("Destination")]
        public string destination { get; set; }
        //
        [Required(ErrorMessage = "Please enter the ETA")]
        [DataType(DataType.Time, ErrorMessage = "Please enter the correct format of ETA hh:mm:ss")]
        [DisplayName("ETA")]
        public TimeSpan eta { get; set; }
    }
}