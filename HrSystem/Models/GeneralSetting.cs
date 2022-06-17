using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
    public class GeneralSetting
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="Bouns Is Required Field")]
        [Display(Name = "Bouns")]

        public float Extra { get; set; }
        [Required(ErrorMessage = "Deduction Is Required Field")]
        [Display(Name = "Deduction")]

        public float Discount { get; set; }
        [Required(ErrorMessage = "Fitsr Day Is Required Field")]
        [Display(Name = "First Day Off")]

        public string Dayoff_1 { get; set; }
        [Required(ErrorMessage = "Second Day Is Required Field")]
        [Display(Name ="Second Day Off")]

        public string Dayoff_2 { get; set; }
        

    }
}
